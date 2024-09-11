using financial_manager.Entities;
using financial_manager.Entities.Requests;

namespace financial_manager.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task RegisterAsync(RegisterRequest request);
        Task<TokenResponse> LoginAsync(LoginRequest request);
        Task<TokenResponse> RefreshTokensAsync(string refreshToken);
        Task LogoutAsync(string refreshToken);
    }
}
