using AgroShop.Core.Entities;
using AgroShop.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroShop.Persistence.Configurations
{
    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.HasKey(sc => sc.Id);

            builder.Property(sc => sc.Id).ValueGeneratedNever();
            builder.Property(sc => sc.CategoryId).IsRequired();

            builder.Property(sc => sc.Name).IsRequired().HasColumnType("VARCHAR(200)");

            builder.HasOne(sc => sc.Category).WithMany(c => c.SubCategories).HasForeignKey(sc => sc.CategoryId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(sc => sc.Products).WithOne(p => p.SubCategory).HasForeignKey(p => p.SubCategoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(sc => sc.ProductAttributes).WithOne(pa => pa.SubCategory).HasForeignKey(pa => pa.SubCategoryId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
