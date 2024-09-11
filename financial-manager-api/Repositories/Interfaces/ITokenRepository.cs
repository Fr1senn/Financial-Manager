using financial_manager.Entities.Models;

namespace financial_manager.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        Task CreateTokenAsync(Token authToken);
        Task<Token> GetTokenAsync(string refreshToken);
        Task RevokeTokenAsync(string refreshToken);
        Task RevokeAllUserTokensAsync(int userId);
    }
}
