using DonKeyNielas.DTOs;
namespace DonKeyNielas.DTOs
{
    public class SetGroupMatchResultDto
    {
        public int ChampionshipId { get; set; }
        public int MatchWeek { get; set; }
        public List<UpdateMatchResultDto> Matches { get; set; } = new List<UpdateMatchResultDto>();
    }
}
