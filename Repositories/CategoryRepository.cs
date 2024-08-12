using financial_manager.Entities;
using financial_manager.Models;
using financial_manager.Repositories.Interfaces;
using financial_manager.Services;
using financial_manager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace financial_manager.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FinancialManagerContext _financialManagerContext;
        private readonly ICategoryService _categoryService;

        public CategoryRepository(FinancialManagerContext financialManagerContext, ICategoryService categoryService)
        {
            _financialManagerContext = financialManagerContext;
            _categoryService = categoryService;
        }
        public async Task CreateCategoryAsync(Category category)
        {
            if (category is null)
            {
                throw new NullReferenceException(nameof(Category));
            }

            try
            {
                await _categoryService.GetTransactionCategoryAsync(category.Title);
                throw new Exception("The category you are trying to create already exists");
            }
            catch (NullReferenceException)
            {
                _financialManagerContext.Categories.Add(new CategoryEntity
                {
                    Title = category.Title,
                    UserId = 1
                });

                await _financialManagerContext.SaveChangesAsync();
            }
        }
    }
}
