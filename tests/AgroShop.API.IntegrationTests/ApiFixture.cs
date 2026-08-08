using AgroShop.API.IntegrationTests.Fakes;
using AgroShop.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StackExchange.Redis;
using Testcontainers.PostgreSql;
using Xunit;

namespace AgroShop.API.IntegrationTests
{
    // Boots the real API in-process against a disposable PostgreSQL from
    // Testcontainers. One container and one host per test collection - tests
    // share the database, so each test must seed distinct data and assert on
    // what it seeded, not on global totals it doesn't own... with one
    // exception: the very first collection to run gets a genuinely empty
    // database, which the smoke test relies on for its paging assertions.
    //
    // Same major Postgres version as docker-compose.yml so migrations and
    // query translation are exercised against what production-like dev runs.
    public sealed class ApiFixture : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder()
            .WithImage("postgres:18-alpine")
            .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // The container's connection string replaces the dev one at the
            // configuration level, before DI reads it - migrations included,
            // so the schema is applied to the throwaway database on startup.
            builder.UseSetting("ConnectionStrings:DefaultConnection", _postgres.GetConnectionString());

            builder.ConfigureTestServices(services =>
            {
                // Both Redis registrations go, not just the cache: the
                // multiplexer is registered lazily, and leaving it around
                // would let any future resolve silently connect to the dev
                // Redis from appsettings.Development.json.
                services.RemoveAll<IConnectionMultiplexer>();
                services.RemoveAll<ICacheService>();
                services.AddSingleton<ICacheService, InMemoryCacheService>();
            });
        }

        // Explicit implementations: WebApplicationFactory already exposes its
        // own DisposeAsync, and xunit's lifetime methods would collide with it.
        Task IAsyncLifetime.InitializeAsync() => _postgres.StartAsync();

        async Task IAsyncLifetime.DisposeAsync()
        {
            await base.DisposeAsync();
            await _postgres.DisposeAsync();
        }
    }

    [CollectionDefinition(Name)]
    public sealed class ApiCollection : ICollectionFixture<ApiFixture>
    {
        public const string Name = "api";
    }
}
