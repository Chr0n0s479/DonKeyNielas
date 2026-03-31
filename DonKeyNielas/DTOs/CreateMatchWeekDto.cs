namespace DonKeyNielas.DTOs;

public class CreateMatchWeekDto
{
    public int ChampionshipId { get; set; }
    public int MatchWeek { get; set; }

    public List<MatchTeamsDto> Matches { get; set; }
    
    public DateTime MatchDate { get; set; }
}
