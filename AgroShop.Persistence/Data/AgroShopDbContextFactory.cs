using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AgroShop.Persistence.Data
{
    /// <summary>
    /// Used only by the EF Core CLI tools (migrations / database update) at design time.
    /// The running application configures the DbContext through AddPersistence instead.
    /// The connection string can be overridden with the ConnectionStrings__DefaultConnection
    /// environment variable; otherwise the local development database is used.
    /// </summary>
    public class AgroShopDbContextFactory : IDesignTimeDbContextFactory<AgroShopDbContext>
    {
        public AgroShopDbContext CreateDbContext(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
                ?? "Host=localhost;Port=5432;Database=AgroShopDb;Username=postgres;Password=postgres";

            var optionsBuilder = new DbContextOptionsBuilder<AgroShopDbContext>();
            optionsBuilder.UseNpgsql(connectionString, sql => sql.MigrationsAssembly("AgroShop.Persistence"));

            return new AgroShopDbContext(optionsBuilder.Options);
        }
    }
}
