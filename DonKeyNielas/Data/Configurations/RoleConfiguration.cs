using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DonKeyNielas.Entities;
namespace DonKeyNielas.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("ct_role");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id_role");

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("role_name");

        builder.Property(r => r.Description)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("description");

        builder.Property(u => u.CreatedAt)
            .HasColumnName("created_at");

        builder.Property(u => u.LastUpdated)
            .HasColumnName("last_updated");
    }
}
