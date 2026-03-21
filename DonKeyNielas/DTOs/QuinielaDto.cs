namespace DonKeyNielas.DTOs;

public class QuinielaDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ChampionshipId { get; set; }
    public int MatchWeek { get; set; }
    public string Name { get; set; } = string.Empty;
}