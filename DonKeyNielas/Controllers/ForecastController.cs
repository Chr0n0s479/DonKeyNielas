using DonKeyNielas.DTOs;
using DonKeyNielas.Services;
using Microsoft.AspNetCore.Mvc;

namespace DonKeyNielas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuinielaForecastController : ControllerBase
{
    private readonly QuinielaForecastService _quinielaForecastService;

    public QuinielaForecastController(QuinielaForecastService quinielaForecastService)
    {
        _quinielaForecastService = quinielaForecastService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetQuinielaForecasts(int id)
    {
        var result = await _quinielaForecastService.GetForecastsByQuinielaId(id);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(result.Data);
    }
    [HttpPost]
    public async Task<IActionResult> CreateQuinielaForecast(int quinielaId, List<ForecastDto> forecast)
    {
        var result = await _quinielaForecastService.SaveForecast(quinielaId, forecast);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(result.Data);
    }
}
