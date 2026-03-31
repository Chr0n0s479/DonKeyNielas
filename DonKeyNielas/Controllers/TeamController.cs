using DonKeyNielas.Services;
using Microsoft.AspNetCore.Mvc;

namespace DonKeyNielas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamController : ControllerBase
{
    private readonly TeamService _teamsService;

    public TeamController(TeamService teamService)
    {
        _teamsService = teamService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTeams()
    {
        var result = await _teamsService.GetAllTeamsAsync();

        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result.Data);
    }
}