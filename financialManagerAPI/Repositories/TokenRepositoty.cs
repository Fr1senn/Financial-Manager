using financial_manager.Entities.Models;
using financial_manager.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace financial_manager.Repositories
{
    public class TokenRepositoty : ITokenRepository
    {
        private readonly FinancialManagerContext _financialManagerContext;
        private readonly IConfiguration _configuration;

        public TokenRepositoty(
            FinancialManagerContext financialManagerContext,
            IConfiguration configuration
        )
        {
            _financialManagerContext = financialManagerContext;
            _configuration = configuration;
        }

        public async Task CreateTokenAsync(Token token)
        {
            if (string.IsNullOrEmpty(token.RefreshToken))
            {
                throw new ArgumentNullException(nameof(token.RefreshToken));
            }

            _financialManagerContext.Tokens.Add(
                new Token
                {
                    RefreshToken = token.RefreshToken,
                    ExpirationDate = token.ExpirationDate,
                    UserId = token.User!.Id,
                    IsRevoked = token.IsRevoked,
                }
            );
            await _financialManagerContext.SaveChangesAsync();
        }

        public async Task<Token> GetTokenAsync(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new Exception("Invalid refresh token");
            }

            Token? token = await _financialManagerContext.Tokens
                .Include(at => at.User)
                .Where(at => at.RefreshToken == refreshToken)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (token is null)
            {
                throw new Exception("Auth token does not exist");
            }

            if (token.IsRevoked)
            {
                throw new Exception("Token already revoked");
            }

            return token;
        }

        public async Task RevokeTokenAsync(string refreshToken)
        {
            Token? token = await _financialManagerContext.Tokens.FirstOrDefaultAsync(at => at.RefreshToken == refreshToken);

            if (token is null)
            {
                throw new NullReferenceException("Auth token does not exist");
            }

            token.IsRevoked = true;

            _financialManagerContext.Tokens.Update(token);
            await _financialManagerContext.SaveChangesAsync();
        }

        public async Task RevokeAllUserTokensAsync(int userId)
        {
            List<Token> tokens = await _financialManagerContext.Tokens
                .Where(rt => rt.UserId == userId && !rt.IsRevoked)
                .ToListAsync();

            foreach (var token in tokens)
            {
                token.IsRevoked = true;
            }

            _financialManagerContext.Tokens.UpdateRange(tokens);
            await _financialManagerContext.SaveChangesAsync();
        }
    }
}
