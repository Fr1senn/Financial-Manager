using financial_manager.Entities.Models;

namespace financial_manager.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<Category> GetTransactionCategoryAsync(string categoryTitle);
    }
}
