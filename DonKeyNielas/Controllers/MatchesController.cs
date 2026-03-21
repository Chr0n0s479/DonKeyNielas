using DonKeyNielas.DTOs;
using DonKeyNielas.Services;
using Microsoft.AspNetCore.Mvc;

namespace DonKeyNielas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatchesController : ControllerBase
{
    private readonly MatchService _matchService;

    public MatchesController(MatchService matchService)
    {
        _matchService = matchService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMatches(int championshipId, int matchWeek)
    {
        var result = await _matchService.GetMatchesByMatchWeekAndChampionship(matchWeek, championshipId);

        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result.Data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMatchById(int id)
    {
        var result = await _matchService.GetMatchByIdAsync(id);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMatch(CreateMatchDto dto)
    {
        var result = await _matchService.CreateMatchAsync(dto);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(result.Data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMatch(int id, UpdateMatchDto dto)
    {
        dto.Id = id;
        var result = await _matchService.UpdateMatchAsync(dto);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(result.Data);
    }

    [HttpPut("{id}/result")]
    public async Task<IActionResult> SetMatchResult(int id, UpdateMatchResultDto dto)
    {
        dto.MatchId = id;

        var result = await _matchService.SetMatchResult(dto);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(result.Data);
    }
}