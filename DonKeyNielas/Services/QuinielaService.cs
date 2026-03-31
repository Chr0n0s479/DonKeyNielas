using DonKeyNielas.Common;
using DonKeyNielas.Data;
using DonKeyNielas.DTOs;
using DonKeyNielas.Entities;
using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DonKeyNielas.Services;

public class QuinielaService
{
    private readonly AppDbContext _context;

    public QuinielaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> CreateQuiniela(CreateQuinielaDto dto)
    {
        if(dto.UserId < 1)
            return Result<int>.Fail("UserId must be greater than 0");
        
        if(_context.Users.Find(dto.UserId) == null)
            return Result<int>.Fail($"There is no user with id {dto.UserId}");

        if(dto.ChampionshipId < 1)
            return Result<int>.Fail("ChampionshipId must be greater than 0");

        if(_context.Championships.Find(dto.ChampionshipId) == null)
            return Result<int>.Fail($"There is no championship with id {dto.ChampionshipId}");

        if(dto.MatchWeek < 1 || dto.MatchWeek > 17)
            return Result<int>.Fail("MatchWeek must be between 1 and 17");

        using var transaction = await _context.Database.BeginTransactionAsync();


        var quiniela = new Quiniela
        {
            UserId = dto.UserId,
            ChampionshipId = dto.ChampionshipId,
            MatchWeek = dto.MatchWeek,
            CreatedAt = DateTime.UtcNow
        };

        _context.Quinielas.Add(quiniela);
        await _context.SaveChangesAsync();
        
        foreach(CreateForecastDto forecast in dto.Forecasts)
        {
            QuinielaForecast quinielaForecast = new QuinielaForecast
            {
                QuinielaId = quiniela.Id,
                MatchId = forecast.MatchId,
                Forecast = forecast.Forecast,
                CreatedAt = DateTime.UtcNow

            };
            _context.QuinielasForecasts.Add(quinielaForecast);
        }

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return Result<int>.Ok(quiniela.Id);
    }

    public async Task<Result<List<QuinielaDto>>> GetQuinielasByUserId(int userId)
    {
        if(userId < 1)
            return Result<List<QuinielaDto>>.Fail("UserId must be greater than 0");
        
        if(_context.Users.Find(userId) == null)
            return Result<List<QuinielaDto>>.Fail($"There is no user with id {userId}");

        var quinielas = await _context.Quinielas.Where(q => q.UserId == userId)
            .Select(q => new QuinielaDto
            {
                Id = q.Id,
                UserId = q.UserId,
                ChampionshipId = q.ChampionshipId,
                MatchWeek = q.MatchWeek,
                Name = q.Name,
            }).ToListAsync();
        return Result<List<QuinielaDto>>.Ok(quinielas);
    }

    public async Task<Result<QuinielaDto>> GetUserQuinielaForecast(int championshipId, int matchWeek, int userId)
    {
        if (championshipId < 1)
            return Result<QuinielaDto>.Fail("ChampionshipId must be greater than 0");

        if (matchWeek < 1 || matchWeek > 17)
            return Result<QuinielaDto>.Fail("MatchWeek must be between 1 and 17");

        var championshipExists = await _context.Championships
            .AnyAsync(c => c.Id == championshipId);

        if (!championshipExists)
            return Result<QuinielaDto>.Fail($"There is no championship with id {championshipId}");

        var quiniela = await _context.Quinielas
            .Where(q =>
                q.ChampionshipId == championshipId &&
                q.MatchWeek == matchWeek &&
                q.UserId == userId)
            .Select(q => new QuinielaDto
            {
                Id = q.Id,
                UserId = q.UserId,
                ChampionshipId = q.ChampionshipId,
                MatchWeek = q.MatchWeek,
                Name = q.Name,

                Forecasts = q.Forecasts
                    .Select(f => new ForecastDto
                    {
                        MatchId = f.MatchId,
                        HomeTeam = f.Match.HomeTeam,
                        VisitTeam = f.Match.VisitTeam,
                        Forecast = f.Forecast
                    }).ToList()
            })
            .FirstOrDefaultAsync();

        //if (quiniela == null)
        //    return Result<QuinielaDto>.Fail(
        //        $"There is no quiniela for championship {championshipId} and match week {matchWeek}"
        //    );

        return Result<QuinielaDto>.Ok(quiniela);
    }

    public async Task<Result<List<QuinielaDto>>> GetQuinielasById(int quinielaId)
    {
        if(quinielaId < 1)
            return Result<List<QuinielaDto>>.Fail("QuinielaId must be greater than 0");

        var quiniela = _context.Quinielas.Find(quinielaId);

        if(quiniela == null)
            return Result<List<QuinielaDto>>.Fail($"There is no quiniela with id {quinielaId}");

        return Result<List<QuinielaDto>>.Ok(new List<QuinielaDto>
        {
            new QuinielaDto
            {
                Id = quiniela.Id,
                UserId = quiniela.UserId,
                ChampionshipId = quiniela.ChampionshipId,
                MatchWeek = quiniela.MatchWeek,
                Name = quiniela.Name,
            }
        });



    }
}
