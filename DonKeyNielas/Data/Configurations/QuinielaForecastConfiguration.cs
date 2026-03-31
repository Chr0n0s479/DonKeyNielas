using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DonKeyNielas.Entities;

namespace DonKeyNielas.Data.Configurations;

public class QuinielaForecastConfiguration : IEntityTypeConfiguration<QuinielaForecast>
{
    public void Configure(EntityTypeBuilder<QuinielaForecast> builder)
    {
        builder.ToTable("quiniela_forecast");

        builder.HasKey(qf => qf.Id);

        builder.Property(qf => qf.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id_forecast");

        builder.Property(qf => qf.QuinielaId)
            .IsRequired()
            .HasColumnName("id_quiniela");

        builder.Property(qf => qf.MatchId)
            .IsRequired()
            .HasColumnName("id_match");

        builder.Property(qf => qf.Forecast)
            .IsRequired()
            .HasColumnName("forecast")
            .HasMaxLength(1);

        builder.Property(qf => qf.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(qf => qf.Quiniela)
            .WithMany(q => q.Forecasts)
            .HasForeignKey(qf => qf.QuinielaId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_quiniela_forecast_quiniela");

        builder.HasOne(qf => qf.Match)
            .WithMany()
            .HasForeignKey(qf => qf.MatchId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_quiniela_forecast_match");

        builder.HasIndex(qf => new { qf.QuinielaId, qf.MatchId })
            .IsUnique()
            .HasDatabaseName("uq_quiniela_forecast_match");



    }
}