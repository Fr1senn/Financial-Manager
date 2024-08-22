using financial_manager.Entities;
using financial_manager.Models;
using financial_manager.Repositories.Interfaces;
using financial_manager.Utilities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace financial_manager.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly FinancialManagerContext _financialManagerContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenRepository _tokenRepository;

        public AuthRepository(
            FinancialManagerContext financialManagerContext,
            IPasswordHasher passwordHasher,
            ITokenRepository tokenRepository
            )
        {
            _financialManagerContext = financialManagerContext;
            _passwordHasher = passwordHasher;
            _tokenRepository = tokenRepository;
        }

        public async Task<TokenResponse> LoginAsync(LoginModel loginModel)
        {
            User? user = await _financialManagerContext.Users
                .Where(u => u.Email == loginModel.Email)
                .Select(u => new User
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    Email = u.Email,
                    BudgetUpdateDay = u.BudgetUpdateDay,
                    MonthlyBudget = u.MonthlyBudget,
                    Password = u.HashedPassword,
                    PasswordSalt = u.PasswordSalt
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (user is null)
            {
                throw new NullReferenceException("The user with this e-mail does not exist");
            }

            if (!_passwordHasher.VerifyPassword(loginModel.RawPassword, user.Password, user.PasswordSalt!))
            {
                throw new ArgumentException("Incorrect password is specified");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var accessToken = _jwtUtility.GenerateAccessToken(claims);
            var refreshToken = _jwtUtility.GenerateRefreshToken();

            await _tokenRepository.CreateTokenAsync(new Token
            {
                RefreshToken = refreshToken,
                User = user,
                ExpirationDate = DateTime.Now.AddDays(int.Parse(_configuration["Jwt:RefreshTokenExpirationDays"]!)),
                IsRevoked = false,
            });

            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
