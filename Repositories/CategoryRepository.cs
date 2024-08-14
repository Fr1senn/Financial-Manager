using financial_manager.Entities;
using financial_manager.Models;
using financial_manager.Repositories.Interfaces;
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

        public async Task<IEnumerable<Category>> GetCategoriesAsync(int packSize = 10, int pageNumber = 0)
        {
            if (packSize < 0) throw new ArgumentException("The collection size can only be a non-negative integer");

            if (pageNumber < 0) throw new ArgumentException("The page number can only be a non-negative integer");

            int userId = 1;

            return await _financialManagerContext.Categories
                .Where(c => c.UserId == userId)
                .Skip(packSize * pageNumber)
                .Take(packSize)
                .Select(c => new Category
                {
                    Id = c.Id,
                    Title = c.Title,
                    CreatedAt = c.CreatedAt,
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            if (categoryId < 0) throw new ArgumentException("The identifier can only be a non-negative integer");

            CategoryEntity? category = await _financialManagerContext.Categories.Where(c => c.Id == categoryId).FirstOrDefaultAsync();

            if (category is null)
            {
                throw new NullReferenceException("The provided category does not exist");
            }

            _financialManagerContext.Categories.Remove(category);
            await _financialManagerContext.SaveChangesAsync();
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

        public async Task UpdateCategoryAsync(Category category)
        {
            CategoryEntity? existingCategory = await _financialManagerContext.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);

            if (existingCategory is null)
            {
                throw new NullReferenceException("The category does not exist");
            }

            try
            {
                await _categoryService.GetTransactionCategoryAsync(category.Title);
                throw new Exception("A category with this title already exists");
            }
            catch (NullReferenceException)
            {
                existingCategory.Title = category.Title;

                await _financialManagerContext.SaveChangesAsync();
            }
        }

        public async Task<int> GetUserCategoryQuantity(int userId)
        {
            return (await _financialManagerContext.Categories.Where(c => c.UserId == userId).AsNoTracking().ToListAsync()).Count();
        }
    }
}
