using AgroShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroShop.Persistence.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(rt => rt.Id);

            builder.Property(rt => rt.Id).ValueGeneratedOnAdd();

            builder.Property(rt => rt.UserId).IsRequired();
            builder.Property(rt => rt.TokenHash).IsRequired().HasMaxLength(200);
            builder.Property(rt => rt.IsRevoked).IsRequired().HasDefaultValue(false);
            builder.Property(rt => rt.ExpiryDate).IsRequired();
            builder.Property(rt => rt.RevokeDate).IsRequired(false);

            builder.HasOne(rt => rt.User)
               .WithMany(u => u.RefreshTokens)
               .HasForeignKey(rt => rt.UserId);
        }
    }
}
