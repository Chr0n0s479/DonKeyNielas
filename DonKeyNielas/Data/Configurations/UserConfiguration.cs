using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DonKeyNielas.Entities;
namespace DonKeyNielas.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("ct_user");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("user_id");

        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("username");

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("email");

        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("password_hash");

        builder.Property(u => u.FullName)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("fullname");

        builder.Property(u => u.RoleId)
            .HasColumnName("role_id");

        builder.Property(u => u.CreatedAt)
            .HasColumnName("created_at");

        builder.Property(u => u.LastUpdated)
            .HasColumnName("last_updated");

        builder.Property(u => u.Available)
            .HasColumnName("available");

        builder.HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .HasConstraintName("fk_user_role");
    }
}