using financial_manager.Models;

namespace financial_manager.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync(int packSize = 10, int pageNumber = 0);
        Task DeleteCategoryAsync(int categoryId);
        Task CreateCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
    }
}
