using AgroShop.Core.Entities;
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

            builder.OwnsOne(a => a.Name, nameBuilder =>
            {
                nameBuilder.Property(n => n.Value)
                    .HasMaxLength(50)
                    .HasColumnName("Name")
                    .IsRequired();

                nameBuilder.HasIndex(n => n.Value).IsUnique();
            });

            // Stored as text, not the int ordinal - stays readable/stable if
            // enum members get reordered later (same convention as PackageUnit).
            builder.Property(a => a.ValueType)
                .HasColumnName("ValueType")
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            builder.HasMany(a => a.ProductAttributes).WithOne(pa => pa.Attribute).HasForeignKey(pa => pa.AttributeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(a => a.Options).WithOne(o => o.Attribute).HasForeignKey(o => o.AttributeId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
