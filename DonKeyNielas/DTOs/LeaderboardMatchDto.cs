namespace DonKeyNielas.DTOs
{
    public class LeaderboardMatchDto
    {
        public int MatchId { get; set; }

        public string HomeTeamName { get; set; } = "";
        public string HomeImage { get; set; } = "";
        public int? ScoreHome { get; set; }

        public string VisitTeamName { get; set; } = "";
        public string VisitImage { get; set; } = "";
        public int? ScoreVisit { get; set; }
        public string Result { get; set; } = "";
    }
}
