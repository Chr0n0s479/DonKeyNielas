
using DonKeyNielas.Data;
using DonKeyNielas.DTOs;
using DonKeyNielas.Common;
using Microsoft.EntityFrameworkCore;
namespace DonKeyNielas.Services;

public class ChampionshipService
{

    private readonly AppDbContext _context;

    public ChampionshipService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<ChampionshipDto>>> GetChampionships()
    {
        var championships = await _context.Championships.Select(c => new ChampionshipDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description
        }).ToListAsync();
        return Result<List<ChampionshipDto>>.Ok(championships);

    }

    public async Task<Result<ChampionshipDto>> GetChampionshipById(int championshipId)
    {
        var championship = await _context.Championships.Where(c => c.Id == championshipId).Select(c => new ChampionshipDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description
        }).FirstOrDefaultAsync();

        if (championship == null)
            return Result<ChampionshipDto>.Fail($"There is no championship with id {championshipId}");

        return Result<ChampionshipDto>.Ok(championship);
    }

    public async Task<Result<bool>> CreateChampionship(ChampionshipDto championship)
    {
        if(string.IsNullOrWhiteSpace(championship.Name))
            return  Result<bool>.Fail("Championship name cannot be empty");

        _context.Championships.Add(new Entities.Championship
        {
            Name = championship.Name,
            Description = championship.Description
        });

        await _context.SaveChangesAsync();

        return Result<bool>.Ok(true);

    }

    public async Task<Result<bool>> UpdateChampionship(int id, ChampionshipDto championship)
    {
        if (id <= 0)
            return Result<bool>.Fail("Invalid championship id");

        var championshipToUpdate = await _context.Championships.FindAsync(id);

        if (championshipToUpdate == null)
            return Result<bool>.Fail($"There is no championship with id {id}");

        if (string.IsNullOrWhiteSpace(championship.Name))
            return Result<bool>.Fail("Championship name cannot be empty");

        championshipToUpdate.Name = championship.Name;
        championshipToUpdate.Description = championship.Description;

        await _context.SaveChangesAsync();

        return Result<bool>.Ok(true);

    }
}
