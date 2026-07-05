using AgroShop.Core.Entities;
using AgroShop.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroShop.Persistence.Configurations
{
    public class ProductAttributeValueConfiguration : IEntityTypeConfiguration<ProductAttributeValue>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeValue> builder)
        {
            builder.HasKey(pav => pav.Id);

            builder.Property(pav => pav.Id).ValueGeneratedNever();
            builder.Property(pav => pav.ProductId).IsRequired();
            builder.Property(pav => pav.ProductAttributeId).IsRequired();

            builder.Property(pav => pav.Value).IsRequired().HasColumnType("VARCHAR(100)");

            builder.HasOne(pav => pav.Product).WithMany(p => p.ProductAttributeValues).HasForeignKey(pav => pav.ProductId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(pav => pav.ProductAttribute).WithMany(pa => pa.ProductAttributeValues).HasForeignKey(pa => pa.ProductAttributeId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
