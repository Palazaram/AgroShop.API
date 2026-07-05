using AgroShop.API.Validators.AuthenticationValidators;
using AgroShop.API.Validators.CategoryValidators;
using AgroShop.Application.Jwt;
using AgroShop.Application.Services;
using AgroShop.Core.Interfaces;
using AgroShop.Core.Shared;
using AgroShop.Persistence.Data;
using AgroShop.Persistence.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AgroShop.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents 
                    {
                        OnMessageReceived = context => 
                        {
                            context.Token = context.Request.Cookies["accessToken"];
                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.Scan(scan => scan
                // указываем сборки, где лежат нужные классы
                .FromAssembliesOf(
                    typeof(UserRepository),
                    typeof(AuthService),
                    typeof(IUserRepository),
                    typeof(IJwtTokenHandler), // добавляем сборку с JWT
                    typeof(LoginUserDtoValidator),
                    typeof(AddCategoryDtoValidator)
                )

                // Репозитории
                .AddClasses(c => c.Where(x => x.Name.EndsWith("Repository")))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()

                // Сервисы
                .AddClasses(c => c.Where(x => x.Name.EndsWith("Service")))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()

                // JWT
                .AddClasses(c => c.AssignableTo(typeof(IJwtTokenHandler)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()

                // Валидаторы
                .AddClasses(c => c.AssignableTo(typeof(IValidator<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
            );

            return services;
        }

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AgroShopDbContext>());
            return services;
        }
    }
}
