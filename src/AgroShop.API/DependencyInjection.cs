using AgroShop.API.Filters;
using AgroShop.API.Services;
using AgroShop.API.Validators.CategoryValidators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AgroShop.API
{
    public static class DependencyInjection
    {
        public const string CorsPolicyName = "AllowReact";

        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<ValidationFilter>();
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Register every validator defined in this assembly (authentication and category validators).
            services.AddValidatorsFromAssemblyContaining<AddCategoryDtoValidator>();

            services.AddScoped<IAuthCookieService, AuthCookieService>();

            services.AddJwtAuthentication(configuration);
            services.AddCorsPolicy();

            return services;
        }

        private static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
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
                            context.Token = context.Request.Cookies[AuthCookieService.AccessTokenCookie];
                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }

        private static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName,
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5173")
                              .AllowAnyHeader()                    
                              .AllowAnyMethod()
                              .AllowCredentials();
                    });
            });

            return services;
        }
    }
}
