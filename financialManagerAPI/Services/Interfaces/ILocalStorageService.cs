namespace financial_manager.Services.Interfaces
{
    public interface ILocalStorageService
    {
        Task SetAsync(string key, string value, TimeSpan expiry);
        Task<string?> GetAsync(string key);
    }
}
