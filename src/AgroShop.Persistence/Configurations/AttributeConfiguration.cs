using AgroShop.Core.Entities;
using AgroShop.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Attribute = AgroShop.Core.Entities.Attribute;

namespace AgroShop.Persistence.Configurations
{
    public class AttributeConfiguration : IEntityTypeConfiguration<Attribute>
    {
        public void Configure(EntityTypeBuilder<Attribute> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).ValueGeneratedNever();

            builder.Property(a => a.Name).IsRequired().HasColumnType("VARCHAR(200)");

            builder.HasMany(a => a.ProductAttributes).WithOne(pa => pa.Attribute).HasForeignKey(pa => pa.AttributeId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
