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

            builder.OwnsOne(sc => sc.Name, nameBuilder =>
            {
                nameBuilder.Property(n => n.Value)
                    .HasMaxLength(50)
                    .HasColumnName("Name")
                    .IsRequired();
            });

            // Per-category name uniqueness (a crop like "Огірки" can legitimately
            // recur under different categories) is enforced in SubCategoryService,
            // not here - EF Core's HasIndex can't compose the owner's CategoryId
            // with the owned SubCategoryName.Value in one database index.
            builder.HasOne(sc => sc.Category).WithMany(c => c.SubCategories).HasForeignKey(sc => sc.CategoryId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(sc => sc.Products).WithOne(p => p.SubCategory).HasForeignKey(p => p.SubCategoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(sc => sc.ProductAttributes).WithOne(pa => pa.SubCategory).HasForeignKey(pa => pa.SubCategoryId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
