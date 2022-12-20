using System.Security.Claims;

namespace MinimalApi.Models;

public record AuthenticationData(string? UserName, string? Password);
public record UserData(int Id, string FirstName, string LastName, string UserName, List<Claim> Claims);
