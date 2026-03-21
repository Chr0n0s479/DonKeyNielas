using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DonKeyNielas.Entities;

namespace DonKeyNielas.Data.Configurations;

public class QuinielaConfiguration : IEntityTypeConfiguration<Quiniela>
{
    public void Configure(EntityTypeBuilder<Quiniela> builder)
    {
        builder.ToTable("quiniela");

        builder.HasKey(q => q.Id);

        builder.Property(q => q.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id_quiniela");

        builder.Property(q => q.UserId)
            .IsRequired()
            .HasColumnName("id_user");

        builder.Property(q => q.ChampionshipId)
            .IsRequired()
            .HasColumnName("id_championship");

        builder.Property(q => q.MatchWeek)
            .IsRequired()
            .HasColumnName("match_week");

        builder.Property(q => q.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("name");

        builder.Property(q => q.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(q => q.User)
            .WithMany()
            .HasForeignKey(q => q.UserId)
            .HasConstraintName("fk_quiniela_user");

        builder.HasOne(q => q.Championship)
            .WithMany()
            .HasForeignKey(q => q.ChampionshipId)
            .HasConstraintName("fk_quiniela_championship");


    }
}