using financial_manager.Services.Interfaces;
using StackExchange.Redis;

namespace financial_manager.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private readonly IDatabase _localStorage;

        public LocalStorageService(IConnectionMultiplexer connectionMultiplexer)
        {
            _localStorage = connectionMultiplexer.GetDatabase();
        }

        public async Task SetAsync(string key, string value, TimeSpan expiry)
        {
            await _localStorage.StringSetAsync(key, value, expiry);
        }

        public async Task<string?> GetAsync(string key)
        {
            return await _localStorage.StringGetAsync(key);
        }
    }
}
