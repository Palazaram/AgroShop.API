using AgroShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroShop.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).ValueGeneratedNever();

            builder.OwnsOne(c => c.Name, nameBuilder =>
            {
                nameBuilder.Property(n => n.Value)
                    .HasMaxLength(60)
                    .HasColumnName("Name")
                    .IsRequired();

                nameBuilder
                    .HasIndex(n => n.Value)
                    .IsUnique();
            });

            builder.Property(c => c.ImagePath)
                .HasMaxLength(500)
                .IsRequired();

            builder.HasMany(c => c.SubCategories)
                .WithOne(sc => sc.Category)
                .HasForeignKey(sc => sc.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
