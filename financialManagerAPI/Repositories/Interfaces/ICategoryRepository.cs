using financial_manager.Entities.DTOs;
using financial_manager.Entities.Models;
using financial_manager.Entities.Requests;

namespace financial_manager.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryDTO>> GetCategoriesAsync(PageRequest request);
        Task<int> GetUserCategoryQuantity();
        Task<Dictionary<string, decimal>> GetTotalCategoriesConsumption();
        Task<(IEnumerable<CategoryDTO>, int)> GetCategoriesAsync(PageRequest request);
        Task DeleteCategoryAsync(int categoryId);
        Task CreateCategoryAsync(CategoryRequest request);
        Task UpdateCategoryAsync(CategoryRequest request);
    }
}
