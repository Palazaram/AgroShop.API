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
            // Must exist before WebApplication resolves WebRootPath/the static files
            // provider below - otherwise both stay null/unusable for the app's whole
            // lifetime, even after ImageStorageService creates the folder later at runtime.
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"));

            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddPersistence(builder.Configuration)
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

            // Serves uploaded category/product images from wwwroot (e.g. /images/categories/...).
            app.UseStaticFiles();

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.None,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always
            });

            app.UseRouting();
            app.UseCors(DependencyInjection.CorsPolicyName);

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
