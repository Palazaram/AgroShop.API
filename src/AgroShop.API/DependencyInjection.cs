using AgroShop.API.Extensions;
using AgroShop.API.Filters;
using AgroShop.API.Services;
using AgroShop.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

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

                // Drops the synthetic "The <param> field is required" error added for non-nullable
                // reference type parameters when the body fails to bind - it's noise on top of the
                // real per-property error (e.g. a JSON type-conversion failure).
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                // [ApiController]'s automatic model-state validation runs before ValidationFilter
                // or the action itself, so it needs its own conversion to the Envelope error shape.
                options.InvalidModelStateResponseFactory = context =>
                    context.ModelState.ToValidationErrorResponse();
            })
            .AddJsonOptions(options =>
            {
                // Enums serialize/bind as their name ("SingleSelect"), not the ordinal -
                // matches how they're already stored in the database (HasConversion<string>)
                // and means a [FromBody] JSON payload is self-documenting instead of
                // requiring the caller to know which number means what.
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped<IAuthCookieService, AuthCookieService>();
            services.AddScoped<IImageStorageService, ImageStorageService>();

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
