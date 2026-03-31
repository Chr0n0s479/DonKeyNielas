namespace DonKeyNielas.DTOs;

public class QuinielaDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ChampionshipId { get; set; }
    public int MatchWeek { get; set; }
    public List<ForecastDto> Forecasts { get; set; } = new List<ForecastDto>();
    public string Name { get; set; } = string.Empty;
}