using DonKeyNielas.DTOs;
using DonKeyNielas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DonKeyNielas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChampionshipsController : ControllerBase
{
    private readonly ChampionshipService _championshipService;

    public ChampionshipsController(ChampionshipService championshipService)
    {
        _championshipService = championshipService;
    }

    [HttpGet]
    public async Task<IActionResult> GetChampionships()
    {
        var result = await _championshipService.GetChampionships();
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(result.Data);
    }
    [HttpGet("latest")]
    public async Task<IActionResult> GetLatestChampionshipMatchWeek()
    {
        var result = await _championshipService.GetLatestChampionshipMatchWeek();
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(result.Data);
    }
    [HttpGet("{championshipId}/latestByChampionship")]
    public async Task<IActionResult> GetLatestChampionshipMatchWeek(int championshipID)
    {
        var result = await _championshipService.GetLatestChampionshipMatchWeek(championshipID);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(result.Data);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetChampionshipsById(int id)
    {
        var result = await _championshipService.GetChampionshipById(id);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(result.Data);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateChampionship(ChampionshipDto dto)
    {
        var result = await _championshipService.CreateChampionship(dto);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(result.Data);
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateChampionship(int id, ChampionshipDto dto)
    {
        var result = await _championshipService.UpdateChampionship(id, dto);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(result.Data);
    }

}
