using ApiSecurity.Constants;
using Microsoft.AspNetCore.Authorization;

namespace ApiSecurity.ProgramExtensions;

public static class SecurityExtension
{
    public static IServiceCollection AddAuthorizationExtension(this IServiceCollection services)
    {
        services.AddAuthorization(opts =>
        {
            opts.AddPolicy(PolicyConstants.MustBeTheOwner, policy =>
            {
                //policy.RequireUserName("nate");
                policy.RequireClaim("title", "Business Owner");
            });

            opts.AddPolicy(PolicyConstants.MustBeAVeteranEmployee, policy =>
            {
                //policy.RequireUserName("nate");
                policy.RequireClaim("employeeId", "E001", "E002");
            });

            opts.AddPolicy(PolicyConstants.MustHaveEmployeeId, policy =>
            {
                policy.RequireClaim("employeeId");
            });
            opts.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });
        return services;
    }
}