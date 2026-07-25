using System.Text.Json;
using AgroShop.Application.Interfaces;
using StackExchange.Redis;

namespace AgroShop.Infrastructure.Caching
{
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        private IDatabase Database => _connectionMultiplexer.GetDatabase();

        // StackExchange.Redis's IDatabase calls have no CancellationToken overload -
        // it's kept on the interface for consistency with the rest of the codebase's
        // async signatures, just unused here.
        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            var value = await Database.StringGetAsync(key);
            if (!value.HasValue)
                return default;

            return JsonSerializer.Deserialize<T>((string)value!);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null, CancellationToken cancellationToken = default)
        {
            var serialized = JsonSerializer.Serialize(value);
            var expiration = expiry.HasValue ? (Expiration)expiry.Value : Expiration.Default;
            await Database.StringSetAsync(key, serialized, expiration);
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            await Database.KeyDeleteAsync(key);
        }
    }
}
