using DonKeyNielas.Data;
using DonKeyNielas.Entities;
using DonKeyNielas.DTOs;
using DonKeyNielas.Common;
using Microsoft.EntityFrameworkCore;

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

        var quiniela = new Quiniela
        {
            UserId = dto.UserId,
            ChampionshipId = dto.ChampionshipId,
            MatchWeek = dto.MatchWeek,
            Name = dto.Name,
            CreatedAt = DateTime.Now
        };


        _context.Quinielas.Add(quiniela);

        await _context.SaveChangesAsync();

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
