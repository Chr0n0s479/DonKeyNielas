namespace DonKeyNielas.Entities;

public class QuinielaForecast
{
    public int Id { get; set; }
    public int QuinielaId { get; set; }
    public Quiniela Quiniela { get; set; } = null!;
    public int MatchId { get; set; }
    public Match Match { get; set; } = null!;   
    public char Forecast { get; set; }
    public DateTime CreatedAt { get; set; }
}
