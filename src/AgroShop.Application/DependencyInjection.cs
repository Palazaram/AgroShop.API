using AgroShop.Application.Interfaces;
using AgroShop.Application.Jwt;
using AgroShop.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AgroShop.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISubCategoryService, SubCategoryService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJwtTokenHandler, JwtTokenHandler>();

            return services;
        }
    }
}
