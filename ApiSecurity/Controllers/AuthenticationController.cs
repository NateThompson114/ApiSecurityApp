using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ApiSecurity.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _config;

    public AuthenticationController(IConfiguration config)
    {
        _config = config;
    }
    public record AuthenticationData(string? UserName, string? Password);

    public record UserData(int UserId, string UserName, string Title, string? EmployeeId);

    // api/Authentication/token
    [AllowAnonymous]
    [HttpPost("Token")]
    public ActionResult<string> Authenticate([FromBody] AuthenticationData data)
    {
        var user = ValidateCredentials(data);

        if (user is null) return Unauthorized();

        var token = GenerateToken(user);

        return Ok(token);
    }

    private string GenerateToken(UserData user)
    {
        var secretKey = new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(_config.GetValue<string>("Authentication:SecretKey"))
                );
        
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        //var claims = new List<Claim>();
        //claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()));
        //claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName));

        var claims = new List<Claim?>
        {
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new("title", user.Title),
            !string.IsNullOrWhiteSpace(user.EmployeeId) ? new Claim("employeeId", user.EmployeeId) : null,
        };

        var token = new JwtSecurityToken(
            _config.GetValue<string>("Authentication:Issuer"), 
            _config.GetValue<string>("Authentication:Audience"),
            claims,
            DateTime.UtcNow, // When this token becomes valid (can create a new token)
            DateTime.UtcNow.AddMinutes(1), // When the token will expire
            signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private UserData? ValidateCredentials(AuthenticationData data)
    {
        // THIS IS NOT PRODUCTION CODE - DEMO - DO NOT USE - REPLACE WITH REAL SYSTEM
        if (CompareValues(data.UserName, "nate") && CompareValues(data.Password, "Test123"))
            return new UserData(1, data.UserName!, "Developer", "E002");
        if (CompareValues(data.UserName, "misty") && CompareValues(data.Password, "Test123"))
            return new UserData(1, data.UserName!, "Business Owner", "E001");
        if (CompareValues(data.UserName, "river") && CompareValues(data.Password, "Test123"))
            return new UserData(1, data.UserName!, "Dog Watcher", "E003");
        if (CompareValues(data.UserName, "joe") && CompareValues(data.Password, "Test123"))
            return new UserData(1, data.UserName!, "Social Media", null);
        return null;
    }

    private bool CompareValues(string? actual, string expected) => 
        actual is not null && actual.Equals(expected);
}