using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using financial_manager.Entities;
using financial_manager.Models;
using financial_manager.Repositories.Interfaces;
using financial_manager.Utilities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace financial_manager.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FinancialManagerContext _financialManagerContext;
        private readonly IPasswordHasher _passwordHasher;

        public UserRepository(FinancialManagerContext financialManagerContext, IPasswordHasher passwordHasher)
        {
            _financialManagerContext = financialManagerContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> GetCurrentUserCredentialsAsync()
        {
            int userId = 1;
            User? currentUser = await _financialManagerContext.Users.Where(u => u.Id == userId).Select(u => new User
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                RegistrationDate = u.RegistrationDate,
                MonthlyBudget = u.MonthlyBudget,
                BudgetUpdateDay = u.BudgetUpdateDay
            }).AsNoTracking().FirstOrDefaultAsync();

            if (currentUser is null)
            {
                throw new NullReferenceException("User does not exist");
            }

            return currentUser;
        }

        public async Task CreateUserAsync(User user)
        {
            string? existingEmail = await _financialManagerContext.Users
                .Where(u => u.Email == user.Email)
                .Select(u => u.Email)
                .FirstOrDefaultAsync();

            if (existingEmail != null)
            {
                throw new ArgumentException("A user with this e-mail address already exists");
            }

            var userSalt = _passwordHasher.GenerateSalt();

            _financialManagerContext.Users.Add(new UserEntity
            {
                FullName = user.FullName,
                Email = user.Email,
                HashedPassword = _passwordHasher.HashPassword(user.Password, userSalt),
                PasswordSalt = userSalt
            });

            await _financialManagerContext.SaveChangesAsync();
        }
    }
}