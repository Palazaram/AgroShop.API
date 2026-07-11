using AgroShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroShop.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id).ValueGeneratedNever();

            builder.OwnsOne(u => u.LastName, lastNameBuilder =>
            {
                lastNameBuilder.Property(ln => ln.Value)
                    .HasMaxLength(50)
                    .HasColumnName("LastName")
                    .IsRequired();
            });

            builder.OwnsOne(u => u.FirstName, firstNameBuilder =>
            {
                firstNameBuilder.Property(fn => fn.Value)
                    .HasMaxLength(50)
                    .HasColumnName("FirstName")
                    .IsRequired();
            });

            builder.OwnsOne(u => u.Patronymic, patronymicBuilder =>
            {
                patronymicBuilder.Property(p => p.Value)
                    .HasMaxLength(50)
                    .HasColumnName("Patronymic")
                    .IsRequired();
            });
            builder.Navigation(u => u.Patronymic).IsRequired();

            builder.OwnsOne(u => u.Email, emailBuilder =>
            {
                emailBuilder.Property(e => e.Value)
                    .HasMaxLength(255)
                    .HasColumnName("Email")
                    .IsRequired();

                emailBuilder
                    .HasIndex(e => e.Value)
                    .IsUnique();
            });

            builder.OwnsOne(u => u.Phone, phoneBuilder =>
            {
                phoneBuilder.Property(e => e.Value)
                    .HasColumnType("char(9)")
                    .HasColumnName("Phone")
                    .IsRequired();

                phoneBuilder
                    .HasIndex(e => e.Value)
                    .IsUnique();
            });

            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(u => u.CreatedAtUtc).IsRequired();
            builder.Property(u => u.RoleId).IsRequired();

            builder.HasMany(u => u.RefreshTokens)
               .WithOne(rt => rt.User)
               .HasForeignKey(rt => rt.UserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
