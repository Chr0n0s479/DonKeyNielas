using DonKeyNielas.Common;
using DonKeyNielas.Data;
using DonKeyNielas.DTOs;
using DonKeyNielas.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
namespace DonKeyNielas.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    private string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
        );

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        var claims = new[]
        {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Role, user.Role.Name)
    };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(6),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<Result<string>> Login(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            return Result<string>.Fail("Invalid username or password");

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserName == username && u.Available);

        if (user == null)
            return Result<string>.Fail("Invalid username or password");

        bool validPassword = BCrypt.Net.BCrypt.Verify(password, user.Password);

        if (!validPassword)
            return Result<string>.Fail("Invalid username or password");

        var token = GenerateJwtToken(user);

        return Result<string>.Ok(token);
    }

    public async Task<Result<bool>> RegisterUser(CreateUserDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.UserName))
            return Result<bool>.Fail("Username cannot be empty") ;
            
        if(string.IsNullOrWhiteSpace(dto.Password))
            return Result<bool>.Fail("Password cannot be empty") ;

        if(string.IsNullOrWhiteSpace(dto.FullName))
            return Result<bool>.Fail("Password cannot be empty") ;

        var inviteValid = await _context.InviteCodes.AnyAsync(i => i.Code == dto.InviteCode && i.QtyUsed < i.MaxUses);

        if (!inviteValid)
            return Result<bool>.Fail("Invalid or expired invite code");

        if (dto.UserName.Length < 5)
            return Result<bool>.Fail("Username lenght need to be 5 characters at least") ;

        if(dto.FullName.Length < 5)
            return Result<bool>.Fail("Fullname lenght need to be 5 characters at least") ;

        if(dto.Password.Length < 5)
            return Result<bool>.Fail("Password lenght need to be 5 characters at least") ;
    

        if (await _context.Users.AnyAsync(u => u.UserName == dto.UserName))
            return Result<bool>.Fail("Username is not available") ;

        var newUser = new User
        {
            UserName = dto.UserName,
            FullName = dto.FullName,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Email = dto.Email,
            RoleId = 2,
            CreatedAt = DateTime.UtcNow,
            LastUpdated = DateTime.UtcNow,
            Available = true
        };

        _context.InviteCodes.First(i => i.Code == dto.InviteCode).QtyUsed += 1;


        _context.Users.Add(newUser);
        
        await _context.SaveChangesAsync();
        
        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> ChangePassword(int userId, string currentPassword, string newPassword)
    {
        if (string.IsNullOrWhiteSpace(currentPassword) || string.IsNullOrWhiteSpace(newPassword))
            return Result<bool>.Fail("Current password and new password cannot be empty");

        if (newPassword.Length < 5)
            return Result<bool>.Fail("New password length need to be 5 characters at least");

        var user = await _context.Users.FindAsync(userId);

        if (user == null)
            return Result<bool>.Fail("User not found");

        if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.Password))
            return Result<bool>.Fail("Current password is incorrect");

        user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
        user.LastUpdated = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Result<bool>.Ok(true);
    }

}
