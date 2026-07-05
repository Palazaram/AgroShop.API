using AgroShop.Core.Entities;
using AgroShop.Core.ValueObjects;
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

            builder.Property(s => s.Name).IsRequired().HasColumnType("VARCHAR(200)");

            builder.HasMany(s => s.Products).WithOne(p => p.Supplier).HasForeignKey(p => p.SupplierId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
