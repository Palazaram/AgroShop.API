using AgroShop.Core.Entities;
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
            builder.Property(pav => pav.AttributeOptionId).IsRequired();

            // Same product can't have the same option recorded twice.
            builder.HasIndex(pav => new { pav.ProductId, pav.AttributeOptionId }).IsUnique();

            builder.HasOne(pav => pav.Product).WithMany(p => p.ProductAttributeValues).HasForeignKey(pav => pav.ProductId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(pav => pav.ProductAttribute).WithMany(pa => pa.ProductAttributeValues).HasForeignKey(pav => pav.ProductAttributeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(pav => pav.AttributeOption).WithMany(o => o.ProductAttributeValues).HasForeignKey(pav => pav.AttributeOptionId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
