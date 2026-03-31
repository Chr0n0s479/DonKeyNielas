using DonKeyNielas.Entities;

namespace DonKeyNielas.DTOs;

public class ForecastDto
{
    public int MatchId { get; set; }
    public Team HomeTeam { get; set; } = null!;
    public Team VisitTeam { get; set; } = null!;
    public string Forecast { get; set; } = String.Empty;
}
