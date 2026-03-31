using DonKeyNielas.Services;
using Microsoft.AspNetCore.Mvc;

namespace DonKeyNielas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaderboardController : ControllerBase
{
    private readonly LeaderboardService _leaderboardService;

    public LeaderboardController(LeaderboardService leaderboardService)
    {
        _leaderboardService = leaderboardService;
    }

    [HttpGet("championship/{championshipId}/week/{matchWeek}")]
    public async Task<IActionResult> GetLeaderboardByConfig(int championshipId, int matchWeek)
    {
        var result = await _leaderboardService.GetLeaderboardByConfig(championshipId, matchWeek);

        if(!result.Success)
            return BadRequest(result.Message);

        return Ok(result.Data);
    }
}
