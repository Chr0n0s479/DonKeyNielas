namespace DonKeyNielas.DTOs;

public class MatchDto
{
    public int Id { get; set; }

    public int MatchWeek { get; set; }

    public DateTime MatchDate { get; set; }

    public int HomeTeamId { get; set; }

    public string HomeTeamName { get; set; } = String.Empty;

    public string HomeTeamImage { get; set; } = String.Empty;

    public int VisitTeamId { get; set; }

    public string VisitTeamName { get; set; } = String.Empty;

    public string VisitTeamImage { get; set; } = String.Empty;

    public int? ScoreHomeTeam { get; set; }

    public int? ScoreVisitTeam { get; set; }
}
