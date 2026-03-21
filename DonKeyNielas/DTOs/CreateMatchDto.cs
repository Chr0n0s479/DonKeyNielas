namespace DonKeyNielas.DTOs;

public class CreateMatchDto
{
    public int ChampionshipId { get; set; }
    public int MatchWeek { get; set; }
    public int HomeTeamId { get; set; }
    public int VisitTeamId { get; set; }
    public DateTime MatchDate { get; set; }
}
