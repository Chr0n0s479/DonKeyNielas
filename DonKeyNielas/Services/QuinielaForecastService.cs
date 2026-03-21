using DonKeyNielas.Common;
using DonKeyNielas.Data;
using DonKeyNielas.DTOs;
using DonKeyNielas.Entities;
using Microsoft.EntityFrameworkCore;


namespace DonKeyNielas.Services;

public class QuinielaForecastService
{
    private readonly AppDbContext _context;

    public QuinielaForecastService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> SaveForecast(int quinielaId, List<ForecastDto> forecasts)
    {
        List<char> validResults = new List<char> { 'L', 'V', 'E' };

        if (quinielaId < 1)
            return Result<bool>.Fail($"Invalid quiniela id {quinielaId}");

        var quiniela = await _context.Quinielas.FirstOrDefaultAsync(q => q.Id == quinielaId);

        if (quiniela == null)
            return Result<bool>.Fail($"There is no quiniela with id {quinielaId}");

        if (forecasts.Count != 9)
            return Result<bool>.Fail("Forecast must contain 9 matches");

        if (forecasts.Any(f => !validResults.Contains(f.Forecast)))
            return Result<bool>.Fail("Forecast must be L, V or E");

        if (forecasts.Select(f => f.MatchId).Distinct().Count() != forecasts.Count)
            return Result<bool>.Fail("Forecast contains duplicated matches");

        var entities = forecasts.Select(f => new QuinielaForecast
        {
            QuinielaId = quinielaId,
            MatchId = f.MatchId,
            Forecast = f.Forecast,
            CreatedAt = DateTime.UtcNow
        });

        _context.QuinielasForecasts.AddRange(entities);

        await _context.SaveChangesAsync();

        return Result<bool>.Ok(true);
    }

    public async Task<Result<List<ForecastDto>>> GetForecastsByQuinielaId(int quinielaId)
    {
        if (quinielaId < 1)
            return Result<List<ForecastDto>>.Fail($"Invalid quiniela id {quinielaId}");

        if (!await _context.Quinielas.AnyAsync(q => q.Id == quinielaId))
            return Result<List<ForecastDto>>.Fail($"There is no quiniela with id {quinielaId}");

        var forecasts = await _context.QuinielasForecasts
            .Where(qf => qf.QuinielaId == quinielaId)
            .Select(f => new ForecastDto
            {
                MatchId = f.MatchId,
                Forecast = f.Forecast,
            }).ToListAsync();

        return Result<List<ForecastDto>>.Ok(forecasts);
    }
}
