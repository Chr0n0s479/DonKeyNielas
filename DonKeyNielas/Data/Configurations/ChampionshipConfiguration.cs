using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DonKeyNielas.Entities;

namespace DonKeyNielas.Data.Configurations;

public class ChampionshipConfiguration : IEntityTypeConfiguration<Championship>
{
    public void Configure(EntityTypeBuilder<Championship> builder)
    {
        builder.ToTable("championship");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id_championship");

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("name");

        builder.Property(c => c.Description)
            .HasMaxLength(255)
            .HasColumnName("description");

        builder.Property(c => c.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(c => c.LastUpdated)
            .HasColumnName("last_updated");

        builder.HasMany(c => c.Matches)
            .WithOne(m => m.Championship)
            .HasForeignKey(m => m.ChampionshipId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_match_championship");

    }
}