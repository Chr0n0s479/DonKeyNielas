using DonKeyNielas.Common;
using DonKeyNielas.Data;
using DonKeyNielas.DTOs;
using DonKeyNielas.Entities;
using Microsoft.EntityFrameworkCore;

namespace DonKeyNielas.Services
{
    public class LeaderboardService
    {
        private readonly AppDbContext _context;

        public LeaderboardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<LeaderboardDto>> GetLeaderboardByConfig(int championshipId, int matchWeek)
        {
            if (championshipId < 1)
                return Result<LeaderboardDto>.Fail("ChampionshipId must be greater than 0");

            if (!_context.Championships.Any(c => c.Id == championshipId))
                return Result<LeaderboardDto>.Fail($"There is no championship with id {championshipId}");

            if (matchWeek < 1 || matchWeek > 17)
                return Result<LeaderboardDto>.Fail("MatchWeek must be between 1 and 17");

            if(!_context.Matches.Any(m => m.ChampionshipId == championshipId && m.MatchWeek == matchWeek))
                return Result<LeaderboardDto>.Fail($"There are no matches for championship id {championshipId} and match week {matchWeek}");

            var matches = await _context.Matches.Include(m => m.HomeTeam).Include(m => m.VisitTeam)
                                                            .Where(m => m.ChampionshipId == championshipId &&
                                                                        m.MatchWeek == matchWeek)
                                                            .OrderBy(m => m.Id)
                                                            .ToListAsync();

            var forecasts = await _context.QuinielasForecasts.Include(f => f.Quiniela).ThenInclude(q => q.User)
                                                            .Where(f =>
                                                                f.Quiniela.ChampionshipId == championshipId &&
                                                                f.Quiniela.MatchWeek == matchWeek)
                                                            .ToListAsync();
            var matchDtos = matches.Select(m => new LeaderboardMatchDto
            {
                MatchId = m.Id,
                HomeTeamName = m.HomeTeam.Name,
                HomeImage = m.HomeTeam.ImageFile,
                VisitTeamName = m.VisitTeam.Name,
                VisitImage = m.VisitTeam.ImageFile,
                ScoreHome = m.ScoreHomeTeam,
                ScoreVisit = m.ScoreVisitTeam,
                Result = GetMatchResult(m)
            }).ToList();

            //var matchResults = matches.ToDictionary(m => m.Id, m => GetMatchResult(m));

            // 1. Calculamos los resultados REALES una sola vez.
            // Esto nos da un mapa estático: [IdPartido] -> ResultadoReal (ej: "Local", "Empate")
            var matchResults = matches.ToDictionary(m => m.Id, m => GetMatchResult(m));

            var users = forecasts
                .GroupBy(f => f.QuinielaId)
                .Select(g =>
                {
                    var first = g.First();

                    int hits = g.Count(f =>
                        matchResults.ContainsKey(f.MatchId) && matchResults[f.MatchId] == f.Forecast
                    );

                    return new LeaderboardUserDto
                    {
                        UserId = first.Quiniela.User.Id,
                        UserName = first.Quiniela.User.UserName,
                        Hits = hits, 
                        Forecasts = g.Select(f => new LeaderboardForecastDto
                        {
                            MatchId = f.MatchId,
                            Forecast = f.Forecast
                        }).ToList()
                    };
                })
                .OrderByDescending(x => x.Hits)
                .ToList();
            var leaderboard = new LeaderboardDto
            {
                Matches = matchDtos,
                Users = users
            };

            return Result<LeaderboardDto>.Ok(leaderboard);

        }

        private string GetMatchResult(Match m)
        {
            if (m.ScoreHomeTeam == null || m.ScoreVisitTeam == null)
                return String.Empty;

            if (m.ScoreHomeTeam > m.ScoreVisitTeam)
                return "L";

            if (m.ScoreHomeTeam < m.ScoreVisitTeam)
                return "V";

            return "E";
        }
    }
}
