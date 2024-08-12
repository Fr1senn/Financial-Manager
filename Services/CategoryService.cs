using financial_manager.Entities;
using financial_manager.Models;
using financial_manager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace financial_manager.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly FinancialManagerContext _financialManagerContext;

        public CategoryService(FinancialManagerContext financialManagerContext)
        {
            _financialManagerContext = financialManagerContext;
        }

        public async Task<Category> GetTransactionCategoryAsync(string categoryTitle)
        {
            Category? category = await _financialManagerContext.Categories
                .Where(c => c.Title == categoryTitle && c.UserId == 1)
                .Select(c => new Category
                {
                    Id = c.Id,
                    Title = c.Title,
                    CreatedAt = c.CreatedAt,
                })
                .FirstOrDefaultAsync();

            if (category is null)
            {
                throw new NullReferenceException("The provided category does not exist");
            }

            return category;
        }
    }
}
