using System.Security.Claims;
using financial_manager.Entities;
using financial_manager.Entities.Models;
using financial_manager.Entities.Requests;
using financial_manager.Repositories.Interfaces;
using financial_manager.Utilities;
using financial_manager.Utilities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace financial_manager.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly FinancialManagerContext _financialManagerContext;
        private readonly IJWTProvider _jwtUtility;
        private readonly IConfiguration _configuration;
        private readonly ITokenRepository _tokenRepository;

        public AuthRepository(
            FinancialManagerContext financialManagerContext,
            IJWTProvider jwtUtility,
            IConfiguration configuration,
            ITokenRepository tokenRepository
        )
        {
            _financialManagerContext = financialManagerContext;
            _jwtUtility = jwtUtility;
            _configuration = configuration;
            _tokenRepository = tokenRepository;
        }

        public async Task RegisterAsync(RegisterRequest request)
        {
            User? dbUser = await _financialManagerContext.Users.FirstOrDefaultAsync(u =>
                u.Email == request.Email
            );

            if (dbUser != null)
            {
                throw new Exception("A user with this email already exists");
            }

            var salt = PasswordHasher.GenerateSalt();

            _financialManagerContext.Users.Add(
                new User
                {
                    FullName = request.FullName,
                    Email = request.Email,
                    HashedPassword = PasswordHasher.HashPassword(request.Password, salt),
                    PasswordSalt = salt,
                }
            );

            await _financialManagerContext.SaveChangesAsync();
        }

        public async Task<TokenResponse> LoginAsync(LoginRequest request)
        {
            User? dbUser = await _financialManagerContext
                .Users.Where(u => u.Email == request.Email)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (dbUser is null)
            {
                throw new Exception("The user with this e-mail does not exist");
            }

            if (
                !PasswordHasher.VerifyPassword(
                    request.Password,
                    dbUser.HashedPassword,
                    dbUser.PasswordSalt
                )
            )
            {
                throw new Exception("Incorrect password is specified");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
            };

            var accessToken = _jwtUtility.GenerateAccessToken(claims);
            var refreshToken = _jwtUtility.GenerateRefreshToken();

            await _tokenRepository.CreateTokenAsync(
                new Token
                {
                    RefreshToken = refreshToken,
                    User = dbUser,
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
                throw new Exception("Invalid refresh token");
            }

            Token token = await _tokenRepository.GetTokenAsync(refreshToken);

            if (token.IsRevoked || token.ExpirationDate <= DateTime.Now)
            {
                throw new Exception("The refresh token is either revoked or expired");
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
                throw new Exception("Refresh token not found");
            }
            else
            {
                await _tokenRepository.RevokeAllUserTokensAsync(authToken.User!.Id);
            }
        }
    }
}
