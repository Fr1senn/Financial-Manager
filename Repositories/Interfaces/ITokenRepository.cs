using financial_manager.Entities;
using financial_manager.Models;

namespace financial_manager.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        Task CreateTokenAsync(Token authToken);
        Task<Token> GetTokenAsync(string refreshToken);
        Task RevokeTokenAsync(string refreshToken);
        Task RevokeAllUserTokensAsync(int userId);
        Task StoreLastRevokedAccessTokenAsync(string accessToken);
        Task<bool> IsAccessTokenBlacklistedAsync(string accessToken);
    }
}
