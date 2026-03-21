using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace DonKeyNielas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login(string username, string password)
    {
        // Aquí deberías implementar la lógica de autenticación real
        if (username == "admin" && password == "password")
        {
            // Generar un token JWT o similar aquí
            return Ok(new { Token = "fake-jwt-token" });
        }
        return Unauthorized();
    }

    [HttpPost("register")]
    public IActionResult Register(string username, string password, string fullname, string email, string inviteCode)
    {
        // Aquí deberías implementar la lógica de registro real
        // Por ejemplo, guardar el usuario en la base de datos
        return Ok(new { Message = "User registered successfully" });
    }
}
