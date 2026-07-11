using AgroShop.Core.Interfaces;
using AgroShop.Core.Shared;
using AgroShop.Persistence.Data;
using AgroShop.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AgroShop.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
        {
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
    }
}
