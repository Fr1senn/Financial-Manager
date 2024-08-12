namespace financial_manager.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task CreateCategoryAsync(Category category);
    }
}
