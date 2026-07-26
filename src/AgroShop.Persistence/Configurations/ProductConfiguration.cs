using AgroShop.Core.Entities;
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

            builder.OwnsOne(p => p.Name, nameBuilder =>
            {
                nameBuilder.Property(n => n.Value)
                    .HasMaxLength(50)
                    .HasColumnName("Name")
                    .IsRequired();
            });

            // Sku is globally unique - unlike SubCategory.Name (scoped to a
            // category), this is a single-column index on the owned property
            // alone, which EF Core's lambda-based HasIndex can express fine.
            builder.OwnsOne(p => p.Sku, skuBuilder =>
            {
                skuBuilder.Property(s => s.Value)
                    .HasMaxLength(20)
                    .HasColumnName("Sku")
                    .IsRequired();

                skuBuilder.HasIndex(s => s.Value).IsUnique();
            });

            builder.OwnsOne(p => p.Price, priceBuilder =>
            {
                priceBuilder.Property(m => m.Value)
                    .HasColumnName("Price")
                    .HasColumnType("DECIMAL(18,2)")
                    .IsRequired();
            });

            builder.OwnsOne(p => p.StockQuantity, stockBuilder =>
            {
                stockBuilder.Property(s => s.Value)
                    .HasColumnName("StockQuantity")
                    .IsRequired();
            });

            builder.OwnsOne(p => p.Description, descriptionBuilder =>
            {
                descriptionBuilder.Property(d => d.Value)
                    .HasMaxLength(400)
                    .HasColumnName("Description")
                    .IsRequired();
            });

            builder.Property(p => p.IsActive).IsRequired().HasColumnType("BOOLEAN");
            builder.Property(p => p.ImagePath).HasColumnType("VARCHAR(200)").IsRequired();

            builder.Property(p => p.CreatedAtUtc).IsRequired();
            builder.Property(p => p.UpdatedUtc).IsRequired(false);

            // Derived from StockQuantity, not a real column.
            builder.Ignore(p => p.IsAvailable);

            builder.HasOne(p => p.SubCategory)
                .WithMany(sс => sс.Products)
                .HasForeignKey(p => p.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.ProductAttributeValues)
                .WithOne(pav => pav.Product)
                .HasForeignKey(pav => pav.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
