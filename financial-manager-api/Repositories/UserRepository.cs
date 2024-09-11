using System.Security.Claims;
using financial_manager.Entities.Extentions;
using financial_manager.Entities.Models;
using financial_manager.Repositories.Interfaces;
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

        public async Task<User> GetCurrentUserCredentialsAsync()
        {
            int userId = _httpContextAccessor.HttpContext!.GetUserId();
            User? currentUser = await _financialManagerContext
                .Users.Where(u => u.Id == userId)
                .Select(u => new User
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

        public async Task UpdateUserCredentialsAsync(User user)
        {
            int userId = _httpContextAccessor.HttpContext!.GetUserId();
            User? dbUser = await _financialManagerContext.Users.FirstOrDefaultAsync(u =>
                u.Id == userId
            );

            if (dbUser == null)
            {
                throw new Exception("User does not exist");
            }

            dbUser.FullName = user.FullName;
            dbUser.MonthlyBudget = user.MonthlyBudget;
            dbUser.BudgetUpdateDay = user.BudgetUpdateDay;

            await _financialManagerContext.SaveChangesAsync();
        }
    }
}
