namespace DonKeyNielas.DTOs;

public class CreateQuinielaDto
{
    public int UserId { get; set; }
    public int ChampionshipId { get; set; }
    public int MatchWeek { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
