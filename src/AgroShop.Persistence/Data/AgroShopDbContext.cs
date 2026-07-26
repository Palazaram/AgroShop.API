using AgroShop.Core.Entities;
using AgroShop.Core.Shared;
using AgroShop.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Attribute = AgroShop.Core.Entities.Attribute;

namespace AgroShop.Persistence.Data
{
    public class AgroShopDbContext : DbContext, IUnitOfWork
    {
        public AgroShopDbContext(DbContextOptions<AgroShopDbContext> options)
            : base(options)
        {

        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<SubCategory> SubCategories => Set<SubCategory>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<Attribute> Attributes => Set<Attribute>();
        public DbSet<AttributeOption> AttributeOptions => Set<AttributeOption>();
        public DbSet<ProductAttribute> ProductAttributes => Set<ProductAttribute>();
        public DbSet<ProductAttributeValue> ProductAttributeValues => Set<ProductAttributeValue>();
        public DbSet<User> Users => Set<User>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<Role> Roles => Set<Role>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        }
    }
}
