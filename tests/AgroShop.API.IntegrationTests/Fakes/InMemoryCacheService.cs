using System.Collections.Concurrent;
using AgroShop.Application.Interfaces;

namespace AgroShop.API.IntegrationTests.Fakes
{
    // Honest in-memory stand-in for the Redis-backed cache. The subject under
    // test is HTTP behaviour, not cache eviction, so expiry is deliberately
    // ignored - a value lives until removed or the host is torn down. Unlike
    // Redis this stores references rather than serialized copies, which is
    // fine for read-only assertions but means tests must never mutate what
    // they read back.
    public sealed class InMemoryCacheService : ICacheService
    {
        private readonly ConcurrentDictionary<string, object?> _store = new();

        public Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) =>
            Task.FromResult(_store.TryGetValue(key, out var value) ? (T?)value : default);

        public Task SetAsync<T>(string key, T value, TimeSpan? expiry = null, CancellationToken cancellationToken = default)
        {
            _store[key] = value;
            return Task.CompletedTask;
        }

        public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            _store.TryRemove(key, out _);
            return Task.CompletedTask;
        }

        // Test-only escape hatch, not part of ICacheService: seeding goes
        // straight through the DbContext, bypassing the services' cache
        // invalidation, so each test class flushes the cache after seeding
        // to guarantee no listing serves a pre-seed answer.
        public void Clear() => _store.Clear();
    }
}
