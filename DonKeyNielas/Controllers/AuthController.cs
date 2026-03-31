using DonKeyNielas.Common;
using DonKeyNielas.DTOs;
using DonKeyNielas.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace DonKeyNielas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.Login(dto);

        if (!result.Success)
            return Unauthorized(result.Message);

        return Ok(new AuthResponseDto
        {
            Token = result.Data
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserDto dto)
    {

        var result = await _authService.RegisterUser(dto);

        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result.Data);
    }
}
