using financial_manager.Models;

namespace financial_manager.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<TokenResponse> LoginAsync(LoginModel loginModel);
    }
}
