using AgroShop.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using AgroShop.API.Extensions;
using AgroShop.API.Middlewares;
using Microsoft.AspNetCore.CookiePolicy;

namespace AgroShop.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            
            builder.Services.AddDbContext<AgroShopDbContext>(options =>
                options.UseNpgsql(connectionString, sqlOption => sqlOption.MigrationsAssembly("AgroShop.Persistence")));

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Application services, repositories and validators
            builder.Services.AddApplicationServices();

            // Unit of work
            builder.Services.AddUnitOfWork();

            // JWT authentication
            builder.Services.AddJwtAuthentication(builder.Configuration);

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReact",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5173")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                    });
            });

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandler>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCookiePolicy(new CookiePolicyOptions 
            {
                MinimumSameSitePolicy = SameSiteMode.None,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always
            });

            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("AllowReact");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();


            app.Run();
        }
    }
}
