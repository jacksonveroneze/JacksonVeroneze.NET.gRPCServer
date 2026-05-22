using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using JacksonVeroneze.NET.GRPCServer.Infrastructure.Configurations;
using JacksonVeroneze.NET.GRPCServer.Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.NET.GRPCServer.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class AuthorizationExtensions
{
    public static IServiceCollection AddAuthorization(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(appConfiguration);

        services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthorizationPolicies.ProfilesCreate, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireAssertion(context =>
                    HasScope(context.User, AuthorizationScopes.ProfilesCreate));
            });

            options.AddPolicy(AuthorizationPolicies.ProfilesActivate, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireAssertion(context =>
                    HasScope(context.User, AuthorizationScopes.ProfilesActivate));
            });

            options.AddPolicy(AuthorizationPolicies.ProfilesInactivate, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireAssertion(context =>
                    HasScope(context.User, AuthorizationScopes.ProfilesInactivate));
            });

            options.AddPolicy(AuthorizationPolicies.ProfilesRead, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireAssertion(context =>
                    HasScope(context.User, AuthorizationScopes.ProfilesRead));
            });
        });

        return services;

        static bool HasScope(ClaimsPrincipal user, string requiredScope)
        {
            var scopeClaims = user.FindAll("scope")
                .Concat(user.FindAll("scp"))
                .SelectMany(claim => claim.Value.Split(' ', StringSplitOptions.RemoveEmptyEntries));

            return scopeClaims.Contains(requiredScope, StringComparer.Ordinal);
        }
    }
}
