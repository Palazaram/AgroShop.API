using AgroShop.API.Middlewares;
using AgroShop.Application;
using AgroShop.Persistence;
using Microsoft.AspNetCore.CookiePolicy;

namespace AgroShop.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services
                .AddPersistence(connectionString)
                .AddApplication()
                .AddPresentation(builder.Configuration);

            var app = builder.Build();

            // Bring the database schema up to date on startup.
            app.Services.ApplyMigrations();

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
            app.UseCors(DependencyInjection.CorsPolicyName);

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();


            app.Run();
        }
    }
}
