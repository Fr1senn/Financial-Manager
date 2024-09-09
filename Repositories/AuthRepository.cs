using System.Security.Claims;
using financial_manager.Entities;
using financial_manager.Models;
using financial_manager.Repositories.Interfaces;
using financial_manager.Utilities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace financial_manager.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly FinancialManagerContext _financialManagerContext;
        private readonly IJwtUtility _jwtUtility;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IConfiguration _configuration;
        private readonly ITokenRepository _tokenRepository;

        public AuthRepository(
            FinancialManagerContext financialManagerContext,
            IJwtUtility jwtUtility,
            IPasswordHasher passwordHasher,
            IConfiguration configuration,
            ITokenRepository tokenRepository
        )
        {
            _financialManagerContext = financialManagerContext;
            _jwtUtility = jwtUtility;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
            _tokenRepository = tokenRepository;
        }

        public async Task<TokenResponse> LoginAsync(LoginModel loginModel)
        {
            User? user = await _financialManagerContext
                .Users.Where(u => u.Email == loginModel.Email)
                .Select(u => new User
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    Email = u.Email,
                    BudgetUpdateDay = u.BudgetUpdateDay,
                    MonthlyBudget = u.MonthlyBudget,
                    Password = u.HashedPassword,
                    PasswordSalt = u.PasswordSalt,
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (user is null)
            {
                throw new NullReferenceException("The user with this e-mail does not exist");
            }

            if (
                !_passwordHasher.VerifyPassword(
                    loginModel.RawPassword,
                    user.Password,
                    user.PasswordSalt!
                )
            )
            {
                throw new ArgumentException("Incorrect password is specified");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var accessToken = _jwtUtility.GenerateAccessToken(claims);
            var refreshToken = _jwtUtility.GenerateRefreshToken();

            await _tokenRepository.CreateTokenAsync(
                new Token
                {
                    RefreshToken = refreshToken,
                    User = user,
                    ExpirationDate = DateTime.Now.AddDays(
                        int.Parse(_configuration["Jwt:RefreshTokenExpirationDays"]!)
                    ),
                    IsRevoked = false,
                }
            );

            return new TokenResponse { AccessToken = accessToken, RefreshToken = refreshToken };
        }

        public async Task<TokenResponse> RefreshTokensAsync(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new ArgumentException("Invalid refresh token");
            }

            Token token = await _tokenRepository.GetTokenAsync(refreshToken);

            if (token.IsRevoked || token.ExpirationDate <= DateTime.Now)
            {
                throw new ArgumentException("The refresh token is either revoked or expired");
            }

            await _tokenRepository.RevokeTokenAsync(token.RefreshToken);

            var newRefreshToken = _jwtUtility.GenerateRefreshToken();
            var newAccessToken = _jwtUtility.GenerateAccessToken(
                new List<Claim> { new Claim(ClaimTypes.NameIdentifier, token.User!.Id.ToString()) }
            );

            Token newAuthToken = new Token
            {
                RefreshToken = newRefreshToken,
                ExpirationDate = DateTime.Now.AddMinutes(
                    int.Parse(_configuration["Jwt:RefreshTokenExpirationDays"]!)
                ),
                IsRevoked = false,
                User = token.User,
            };

            await _tokenRepository.CreateTokenAsync(newAuthToken);

            await _tokenRepository.StoreLastRevokedAccessTokenAsync(_jwtUtility.GetJwt());

            return new TokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            };
        }

        public async Task LogoutAsync(string refreshToken)
        {
            Token authToken = await _tokenRepository.GetTokenAsync(refreshToken);

            if (authToken is null)
            {
                throw new NullReferenceException("Refresh token not found");
            }
            else
            {
                await _tokenRepository.StoreLastRevokedAccessTokenAsync(_jwtUtility.GetJwt());

                await _tokenRepository.RevokeAllUserTokensAsync(authToken.User!.Id);
            }
        }
    }
}
