using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AgroShop.Persistence.Data
{
    /// <summary>
    /// Used only by the EF Core CLI tools (migrations / database update) at design time.
    /// The running application configures the DbContext through AddPersistence instead.
    /// </summary>
    public class AgroShopDbContextFactory : IDesignTimeDbContextFactory<AgroShopDbContext>
    {
        public AgroShopDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? "Host=localhost;Port=5432;Database=AgroShopDb;Username=postgres;Password=postgres";

            var optionsBuilder = new DbContextOptionsBuilder<AgroShopDbContext>();
            optionsBuilder.UseNpgsql(connectionString, sql => sql.MigrationsAssembly("AgroShop.Persistence"));

            return new AgroShopDbContext(optionsBuilder.Options);
        }

        // Reads the same configuration the running API uses: appsettings(.Development).json for
        // local work, with environment variables (e.g. from Docker/CI) taking precedence.
        private static IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder();

            var apiProjectPath = FindApiProjectPath();
            if (apiProjectPath is not null)
            {
                builder.SetBasePath(apiProjectPath)
                    .AddJsonFile("appsettings.json", optional: true)
                    .AddJsonFile("appsettings.Development.json", optional: true);
            }

            // Added last so environment variables override the JSON files.
            builder.AddEnvironmentVariables();

            return builder.Build();
        }

        // Walks up from the current directory to the solution root, then points at the API project
        // so its appsettings files can be read regardless of where the EF command is invoked from.
        private static string? FindApiProjectPath()
        {
            var directory = new DirectoryInfo(Directory.GetCurrentDirectory());

            while (directory is not null)
            {
                if (directory.GetFiles("AgroShop.API.sln").Length > 0)
                    return Path.Combine(directory.FullName, "src", "AgroShop.API");

                directory = directory.Parent;
            }

            return null;
        }
    }
}
