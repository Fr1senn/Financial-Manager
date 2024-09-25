using financial_manager.Entities.DTOs;
using financialManagerAPI.Entities.Requests;

namespace financial_manager.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDTO> GetCurrentUserCredentialsAsync();
        Task UpdateUserCredentialsAsync(UserRequest request);
    }
}
