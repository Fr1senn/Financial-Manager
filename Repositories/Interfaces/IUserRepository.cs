using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using financial_manager.Models;

namespace financial_manager.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetCurrentUserCredentialsAsync();
    }
}