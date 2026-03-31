namespace DonKeyNielas.DTOs
{
    public class LeaderboardDto
    {
        public List<LeaderboardMatchDto> Matches { get; set; } = new();

        public List<LeaderboardUserDto> Users { get; set; } = new();
    }
}
