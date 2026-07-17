using AgroShop.Core.Interfaces;
using AgroShop.Core.Shared;
using AgroShop.Persistence.Data;
using AgroShop.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgroShop.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<AgroShopDbContext>(options =>
                options.UseNpgsql(connectionString, sql => sql.MigrationsAssembly("AgroShop.Persistence")));

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AgroShopDbContext>());

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();

            return services;
        }

        /// <summary>
        /// Applies any pending EF Core migrations. Called on startup so a fresh database
        /// (e.g. the one created by the Docker Compose Postgres service) is brought up to date.
        /// </summary>
        public static void ApplyMigrations(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AgroShopDbContext>();
            context.Database.Migrate();
        }
    }
}
