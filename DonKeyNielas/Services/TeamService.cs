using DonKeyNielas.Data;
using DonKeyNielas.Entities;
using DonKeyNielas.DTOs;
using DonKeyNielas.Common;

using Microsoft.EntityFrameworkCore;

namespace DonKeyNielas.Services;

public class TeamService
{
    private readonly AppDbContext _context;

    public TeamService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<TeamDto>>> GetAllTeamsAsync()
    {
        return  Result<List<TeamDto>>.Ok(_context.Teams.Where(t => t.Available)
            .Select(t => new TeamDto
        {
            Id = t.Id,
            Name = t.Name,
            ImageFile = t.ImageFile
        }).ToList());

    }

    public async Task<Result<bool>> DisableTeam(int teamId)
    {
        var team = await _context.Teams.FindAsync(teamId);

        if (team == null)
            return Result<bool>.Fail($"There is team with id {teamId}");
        
        if(!team.Available)
            return Result<bool>.Fail("Team is already disabled");

        team.Available = false;
        await _context.SaveChangesAsync();
        
        return Result<bool>.Ok(true);
    }
}
