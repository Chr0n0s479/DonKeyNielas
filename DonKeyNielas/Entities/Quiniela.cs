namespace DonKeyNielas.Entities;

public class Quiniela
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int ChampionshipId { get; set; }
    public Championship Championship { get; set; } = null!;
    public int MatchWeek { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
