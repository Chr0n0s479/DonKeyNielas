using DonKeyNielas.Common;
using DonKeyNielas.Data;
using DonKeyNielas.DTOs;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace DonKeyNielas.Services;

public class MatchService
{
    private readonly AppDbContext _context;

    public MatchService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<MatchDto>>> GetMatchesByMatchWeekAndChampionship(int matchWeek, int championshipId)
    {
        var matches = await _context.Matches.Where(m => m.MatchWeek == matchWeek && m.ChampionshipId == championshipId)
            .Select(m => new MatchDto
            {
                Id = m.Id,

                MatchWeek = m.MatchWeek,
                HomeTeamId = m.HomeTeamId,
                MatchDate = m.MatchDate,
                HomeTeamName = m.HomeTeam.Name,
                HomeTeamImage = m.HomeTeam.ImageFile,
                VisitTeamId = m.VisitTeamId,
                VisitTeamName = m.VisitTeam.Name,
                VisitTeamImage = m.VisitTeam.ImageFile,
                ScoreHomeTeam = m.ScoreHomeTeam,
                ScoreVisitTeam = m.ScoreVisitTeam
            }).ToListAsync();
        return Result<List<MatchDto>>.Ok(matches);
    }

    public async Task<Result<MatchDto>> GetMatchByIdAsync(int matchId)
    {
        if (matchId <= 0)
            return Result<MatchDto>.Fail("Invalid match id");

        var match = _context.Matches.Where(m => m.Id == matchId);

        if (match == null)
            return Result<MatchDto>.Fail($"There is no match with id {matchId}");

        var matchDto = match.Select(m => new MatchDto
        {
            Id = m.Id,

            MatchWeek = m.MatchWeek,
            HomeTeamId = m.HomeTeamId,
            MatchDate = m.MatchDate,
            HomeTeamName = m.HomeTeam.Name,
            HomeTeamImage = m.HomeTeam.ImageFile,
            VisitTeamId = m.VisitTeamId,
            VisitTeamName = m.VisitTeam.Name,
            VisitTeamImage = m.VisitTeam.ImageFile,
            ScoreHomeTeam = m.ScoreHomeTeam,
            ScoreVisitTeam = m.ScoreVisitTeam
        });

        return Result<MatchDto>.Ok(matchDto.FirstOrDefault()!);
    }

    public async Task<Result<int>> CreateMatchAsync(CreateMatchDto dto)
    {
        if(dto.ChampionshipId <= 0)
            return Result<int>.Fail("Invalid championship id");
        
        if(_context.Championships.Find(dto.ChampionshipId) == null)
            return Result<int>.Fail($"There is no championship with id {dto.ChampionshipId}");

        if (dto.HomeTeamId == dto.VisitTeamId)
            return Result<int>.Fail("Home team and visit team cannot be the same");
        if(dto.MatchWeek < 1 || dto.MatchWeek > 17)
            return Result<int>.Fail("Match week must be between 1 and 17");


        var match = new Entities.Match
        {
            MatchWeek = dto.MatchWeek,
            HomeTeamId = dto.HomeTeamId,
            VisitTeamId = dto.VisitTeamId,
            MatchDate = dto.MatchDate,
            ChampionshipId = dto.ChampionshipId
        };
        _context.Matches.Add(match);
        await _context.SaveChangesAsync();
        return Result<int>.Ok(match.Id);
    }

    public async Task<Result<bool>> UpdateMatchAsync(UpdateMatchDto dto)
    {
        if(dto.Id <= 0)
            return Result<bool>.Fail("Invalid match id");
        var match = await _context.Matches.FindAsync(dto.Id);

        if (match == null)
            return Result<bool>.Fail($"There is no match with id {dto.Id}");

        if(_context.Teams.Find(dto.HomeTeamId) == null)
            return Result<bool>.Fail($"There is no team with id {dto.HomeTeamId}");

        if(_context.Teams.Find(dto.VisitTeamId) == null)
            return Result<bool>.Fail($"There is no team with id {dto.VisitTeamId}");

        if(_context.Championships.Find(dto.ChampionshipId) == null)
            return Result<bool>.Fail($"There is no championship with id {dto.ChampionshipId}");

        if (dto.MatchWeek < 1 || dto.MatchWeek > 17)
            return Result<bool>.Fail("Match week must be between 1 and 17");

        if(dto.ScoreHomeTeam < 0 || dto.ScoreVisitTeam < 0)
            return Result<bool>.Fail("Scores cannot be negative");

        match.ChampionshipId = dto.ChampionshipId;
        match.MatchWeek = dto.MatchWeek;
        match.HomeTeamId = dto.HomeTeamId;
        match.VisitTeamId = dto.VisitTeamId;
        match.MatchDate = dto.MatchDate;
        match.ScoreHomeTeam = dto.ScoreHomeTeam;
        match.ScoreVisitTeam = dto.ScoreVisitTeam;

        await _context.SaveChangesAsync();

        return Result<bool>.Ok(true);

    }

    public async Task<Result<bool>> SetMatchResult(UpdateMatchResultDto dto)
    {
        if(dto.MatchId <= 0)
            return Result<bool>.Fail("Invalid match id");

        var match = await _context.Matches.FindAsync(dto.MatchId);

        if (match == null)
            return Result<bool>.Fail($"There is no match with id {dto.MatchId}");

        if(dto.HomeScore < 0 || dto.VisitScore < 0)
            return Result<bool>.Fail("Scores cannot be negative");

        match.ScoreHomeTeam = dto.HomeScore;
        match.ScoreVisitTeam = dto.VisitScore;

        await _context.SaveChangesAsync();
        return Result<bool>.Ok(true);

    }


}
