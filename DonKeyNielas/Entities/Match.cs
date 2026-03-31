namespace DonKeyNielas.Entities;
    public class Match
    {
        public int Id { get; set; }
        public int ChampionshipId { get; set; }
        public Championship Championship { get; set; } = null!;
        public int MatchWeek { get; set; }
        public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; } = null!;
        public int VisitTeamId { get; set; }
        public Team VisitTeam { get; set; } = null!;
        public DateTime MatchDate { get; set; }
        public int? ScoreHomeTeam { get; set; }
        public int? ScoreVisitTeam { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdated { get; set; }

    }
