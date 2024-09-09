using financial_manager.Models;

namespace financial_manager.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync(int packSize = 10, int pageNumber = 0);
        Task<int> GetUserCategoryQuantity();
        Task<Dictionary<string, decimal>> GetTotalCategoriesConsumption();
        Task DeleteCategoryAsync(int categoryId);
        Task CreateCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
    }
}