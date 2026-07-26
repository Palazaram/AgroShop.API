using AgroShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroShop.Persistence.Configurations
{
    public class AttributeOptionConfiguration : IEntityTypeConfiguration<AttributeOption>
    {
        public void Configure(EntityTypeBuilder<AttributeOption> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id).ValueGeneratedNever();
            builder.Property(o => o.AttributeId).IsRequired();

            builder.OwnsOne(o => o.Value, valueBuilder =>
            {
                valueBuilder.Property(v => v.Value)
                    .HasMaxLength(100)
                    .HasColumnName("Value")
                    .IsRequired();
            });

            // Per-attribute uniqueness (not global - "Так" can be a valid
            // option under more than one attribute) is enforced in the
            // service layer, same reason as SubCategory's per-category name
            // check: EF Core can't compose an owner scalar with an owned
            // property's value in one database index.

            builder.HasOne(o => o.Attribute).WithMany(a => a.Options).HasForeignKey(o => o.AttributeId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
