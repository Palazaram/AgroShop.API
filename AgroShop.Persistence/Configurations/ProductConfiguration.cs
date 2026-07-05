using AgroShop.Core.Entities;
using AgroShop.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroShop.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(p => p.SubCategoryId).IsRequired();
            builder.Property(p => p.SupplierId).IsRequired();

            builder.Property(p => p.Name).IsRequired().HasColumnType("VARCHAR(300)");
            builder.Property(p => p.Description).IsRequired(false).HasColumnType("VARCHAR(400)");
            builder.Property(p => p.Price).IsRequired().HasColumnType("DECIMAL(18,2)");
            builder.Property(p => p.IsAvailable).IsRequired().HasColumnType("BOOLEAN");
            builder.Property(p => p.ImagePath).HasColumnType("VARCHAR(200)");

            builder.HasOne(p => p.SubCategory).WithMany(sс => sс.Products).HasForeignKey(p => p.SubCategoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.Supplier).WithMany(s => s.Products).HasForeignKey(p => p.SupplierId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.ProductAttributeValues).WithOne(pav => pav.Product).HasForeignKey(pav => pav.ProductId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
