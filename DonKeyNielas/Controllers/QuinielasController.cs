
using DonKeyNielas.DTOs;
using DonKeyNielas.Entities;
using DonKeyNielas.Services;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace DonKeyNielas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuinielasController : ControllerBase
{
    private readonly QuinielaService _quinielaService;

    public QuinielasController(QuinielaService quinielaService)
    {
        _quinielaService = quinielaService;
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetQuinielasByUserId(int userId)
    {
        var result = await _quinielaService.GetQuinielasByUserId(userId);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(result.Data);
    }
    [HttpGet("{quinielaId}")]
    public async Task<IActionResult> GetQuinielaById(int quinielaId)
    {
        var result = await _quinielaService.GetQuinielasById(quinielaId);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(result.Data);
    }
    [HttpGet("{championshipId}/{matchWeek}/mine")]
    public async Task<IActionResult> GetQuinielaForecastById(int championshipId, int matchWeek)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await _quinielaService.GetUserQuinielaForecast(championshipId, matchWeek, userId);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<IActionResult> CreateQuiniela([FromBody] CreateQuinielaDto dto)
    {

        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .Select(x => new
                {
                    Field = x.Key,
                    Errors = x.Value!.Errors.Select(e => e.ErrorMessage).ToList()
                });

            return BadRequest(errors);
        }


        dto.UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await _quinielaService.CreateQuiniela(dto);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(result.Data);
    }

}
