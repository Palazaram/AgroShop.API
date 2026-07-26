using AgroShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroShop.Persistence.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id).ValueGeneratedNever();

            builder.OwnsOne(s => s.Name, nameBuilder =>
            {
                nameBuilder.Property(n => n.Value)
                    .HasMaxLength(50)
                    .HasColumnName("Name")
                    .IsRequired();

                nameBuilder.HasIndex(n => n.Value).IsUnique();
            });

            builder.Property(s => s.ImagePath)
                .HasMaxLength(500);

            builder.HasMany(s => s.Products).WithOne(p => p.Supplier).HasForeignKey(p => p.SupplierId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
