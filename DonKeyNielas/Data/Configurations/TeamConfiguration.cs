using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DonKeyNielas.Entities;
namespace DonKeyNielas.Data.Configurations;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("team");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id_team");

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("name");

        builder.Property(t => t.ImageFile)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("image_file");

        builder.Property(t => t.Available)
            .HasColumnName("available");

    }
}
