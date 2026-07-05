using AgroShop.Core.Entities;
using AgroShop.Core.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroShop.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedNever();
            builder.Property(r => r.Name).HasMaxLength(50).IsRequired();
            builder.HasIndex(r => r.Name).IsUnique();

            builder.HasData(
                new
                {
                    Id = RoleConstants.AdminId,
                    Name = "Admin"
                },
                new
                {
                    Id = RoleConstants.CustomerId,
                    Name = "Customer"
                }
            );
        }
    }
}
