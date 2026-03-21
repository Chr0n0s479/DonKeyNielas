
using DonKeyNielas.DTOs;
using DonKeyNielas.Services;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost]
    public async Task<IActionResult> CreateQuiniela(CreateQuinielaDto dto)
    {
        var result = await _quinielaService.CreateQuiniela(dto);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(result.Data);
    }

}
