namespace DonKeyNielas.DTOs
{
    public class LeaderboardUserDto
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = "";

        public int Hits { get; set; }

        public List<LeaderboardForecastDto> Forecasts { get; set; } = new();
    }

}
