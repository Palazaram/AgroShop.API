using AgroShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroShop.Persistence.Configurations
{
    public class ProductAttributeConfiguration : IEntityTypeConfiguration<ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            builder.HasKey(pa => pa.Id);

            builder.Property(pa => pa.Id).ValueGeneratedNever();
            builder.Property(pa => pa.SubCategoryId).IsRequired();
            builder.Property(pa => pa.AttributeId).IsRequired();

            // Both are plain scalars on this entity (not owned-type
            // properties), so the composite unique index works directly here -
            // unlike SubCategory.Name/AttributeOption.Value, which needed an
            // app-level check instead.
            builder.HasIndex(pa => new { pa.SubCategoryId, pa.AttributeId }).IsUnique();

            builder.HasOne(pa => pa.SubCategory).WithMany(sc => sc.ProductAttributes).HasForeignKey(pa => pa.SubCategoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(pa => pa.Attribute).WithMany(a => a.ProductAttributes).HasForeignKey(pa => pa.AttributeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(pa => pa.ProductAttributeValues).WithOne(pav => pav.ProductAttribute).HasForeignKey(pav => pav.ProductAttributeId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
