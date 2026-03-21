using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DonKeyNielas.Entities;
namespace DonKeyNielas.Data.Configurations;



public class MatchConfiguration : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder.ToTable("match");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id_match");

        builder.Property(m => m.ChampionshipId)
            .IsRequired()
            .HasColumnName("id_championship");

        builder.Property(m => m.MatchWeek)
            .IsRequired()
            .HasColumnName("match_week");

        builder.Property(m => m.HomeTeamId)
            .IsRequired()
            .HasColumnName("id_home_team");

        builder.Property(m => m.VisitTeamId)
            .IsRequired()
            .HasColumnName("id_visit_team");

        builder.Property(m => m.MatchDate)
            .IsRequired()
            .HasColumnName("match_date");

        builder.Property(m => m.ScoreHomeTeam)
            .HasColumnName("score_home");

        builder.Property(m => m.ScoreVisitTeam)
            .HasColumnName("score_visit");

        builder.Property(m => m.CreatedAt)
            .HasColumnName("created_at");

        builder.Property(m => m.LastUpdated)
            .HasColumnName("last_updated");

        builder.HasOne(m => m.Championship)
            .WithMany(c => c.Matches)
            .HasForeignKey(m => m.ChampionshipId)
            .HasConstraintName("fk_match_championship");

        builder.HasOne(m => m.HomeTeam)
            .WithMany()
            .HasForeignKey(m => m.HomeTeamId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_match_home_team");

        builder.HasOne(m => m.VisitTeam)
            .WithMany()
            .HasForeignKey(m => m.VisitTeamId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_match_visit_team");

    }
}
