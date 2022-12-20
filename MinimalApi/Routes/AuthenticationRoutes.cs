using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MinimalApi.Library.Constants;
using MinimalApi.Models;

namespace MinimalApi.Routes;

public static class AuthenticationRoutes
{
    public static void AddAuthenticationEndpoints(this WebApplication app)
    {
        app.MapPost("/api/Token", (IConfiguration config, [FromBody] AuthenticationData data) =>
        {
            var user = ValidateCredentials(data);

            if (user is null)
            {
                return Results.Unauthorized();
            }

            string token = GenerateToken(user, config);

            return Results.Ok(token);
        }).AllowAnonymous();
    }

    private static string GenerateToken(UserData user, IConfiguration config)
    {
        var secretKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(
                config.GetValue<string>("Authentication:SecretKey")));

        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new();
        claims.Add(new(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
        claims.Add(new(JwtRegisteredClaimNames.UniqueName, user.UserName));
        claims.Add(new(JwtRegisteredClaimNames.GivenName, user.FirstName));
        claims.Add(new(JwtRegisteredClaimNames.FamilyName, user.LastName));
        claims.Add(new Claim(PolicyConstants.CanGetOrders, user.Claims.FirstOrDefault(c => c.Type == PolicyConstants.CanGetOrders)?.Value ?? false.ToString()));
        claims.Add(new Claim(PolicyConstants.CanGetCustomers, user.Claims.FirstOrDefault(c => c.Type == PolicyConstants.CanGetCustomers)?.Value ?? false.ToString()));

        var token = new JwtSecurityToken(
            config.GetValue<string>("Authentication:Issuer"),
            config.GetValue<string>("Authentication:Audience"),
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddMinutes(1),
            signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static UserData? ValidateCredentials(AuthenticationData data)
    {
        // THIS IS NOT PRODUCTION CODE - REPLACE THIS WITH A CALL TO YOUR AUTH SYSTEM
        if (CompareValues(data.UserName, "nthompson") &&
            CompareValues(data.Password, "Test123"))
        {
            var claims = new List<Claim> {
                new(PolicyConstants.CanGetOrders, true.ToString()),
                new(PolicyConstants.CanGetCustomers, true.ToString())
            };
            return new UserData(1, "Nate", "Thompson", data.UserName!, claims);
        }

        //if (CompareValues(data.UserName, "sstorm") &&
        //    CompareValues(data.Password, "Test123"))
        //{
        //    return new UserData(2, "Sue", "Storm", data.UserName!);
        //}

        return null;
    }

    private static bool CompareValues(string? actual, string expected)
    {
        if (actual is not null)
        {
            if (actual.Equals(expected))
            {
                return true;
            }
        }

        return false;
    }
}
