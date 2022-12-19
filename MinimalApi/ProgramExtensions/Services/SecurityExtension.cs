using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MinimalApi.Library.Constants;

namespace MinimalApi.ProgramExtensions.Services;

// https://learn.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-6.0
public static class SecurityExtension
{
    public static void AddSecurityRequirementServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration.GetValue<string>("Authentication:Issuer"),
                    ValidAudience = builder.Configuration.GetValue<string>("Authentication:Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.ASCII.GetBytes(
                                builder.Configuration.GetValue<string>("Authentication:SecretKey")))
                };
            });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy(PolicyConstants.MustBeTheOwner, policy =>
            {
                //policy.RequireUserName("nate");
                policy.RequireClaim("title", "Business Owner");
            });

            options.AddPolicy(PolicyConstants.MustBeAVeteranEmployee, policy =>
            {
                policy.RequireClaim("employeeId", "E001", "E002");
            });

            options.AddPolicy(PolicyConstants.MustHaveEmployeeId, policy =>
            {
                policy.RequireClaim("employeeId");
            });

            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });
    }
}
