using financial_manager.Entities;
using financial_manager.Models;

namespace financial_manager.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        Task CreateTokenAsync(Token authToken);
    }
}
