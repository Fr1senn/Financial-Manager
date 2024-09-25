using System.Security.Claims;
using financial_manager.Entities.Models;
using financial_manager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace financial_manager.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly FinancialManagerContext _financialManagerContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryService(
            FinancialManagerContext financialManagerContext,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _financialManagerContext = financialManagerContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Category> GetTransactionCategoryAsync(string categoryTitle)
        {
            int userId = Convert.ToInt32(
                _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            );

            Category? category = await _financialManagerContext
                .Categories.Where(c => c.Title == categoryTitle && c.UserId == userId)
                .FirstOrDefaultAsync();

            if (category is null)
            {
                throw new NullReferenceException("The provided category does not exist");
            }

            return category;
        }
    }
}
