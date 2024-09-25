using System.Security.Claims;
using financial_manager.Entities.DTOs;
using financial_manager.Entities.Extentions;
using financial_manager.Entities.Models;
using financial_manager.Repositories.Interfaces;
using financialManagerAPI.Entities.Requests;
using Microsoft.EntityFrameworkCore;

namespace financial_manager.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FinancialManagerContext _financialManagerContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(
            FinancialManagerContext financialManagerContext,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _financialManagerContext = financialManagerContext;

            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserDTO> GetCurrentUserCredentialsAsync()
        {
            int userId = _httpContextAccessor.HttpContext!.GetUserId();
            UserDTO? currentUser = await _financialManagerContext
                .Users.Where(u => u.Id == userId)
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    Email = u.Email,
                    RegistrationDate = u.RegistrationDate,
                    MonthlyBudget = u.MonthlyBudget,
                    BudgetUpdateDay = u.BudgetUpdateDay,
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (currentUser is null)
            {
                throw new Exception("User does not exist");
            }

            return currentUser;
        }

        public async Task UpdateUserCredentialsAsync(UserRequest request)
        {
            int userId = _httpContextAccessor.HttpContext!.GetUserId();
            User? dbUser = await _financialManagerContext.Users.FirstOrDefaultAsync(u =>
                u.Id == userId
            );

            if (dbUser == null)
            {
                throw new Exception("User does not exist");
            }

            if (dbUser.Email != request.Email)
            {
                User? userWithRequestEmail = await _financialManagerContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

                if (userWithRequestEmail != null)
                {
                    throw new Exception("The specified email is already in use");
                }
            }

            dbUser.FullName = request.FullName;
            dbUser.Email = request.Email;
            dbUser.MonthlyBudget = request.MonthlyBudget;
            dbUser.BudgetUpdateDay = request.BudgetUpdateDay;

            await _financialManagerContext.SaveChangesAsync();
        }
    }
}
