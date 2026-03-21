namespace DonKeyNielas.DTOs;


public class UpdateMatchDto
{
    public int Id { get; set; }
    public int ChampionshipId { get; set; }
    public int HomeTeamId { get; set; }
    public int VisitTeamId { get; set; }
    public DateTime MatchDate { get; set; }
    public int ScoreHomeTeam { get; set; }
    public int ScoreVisitTeam { get; set; }
    public int MatchWeek { get; set; }
}
