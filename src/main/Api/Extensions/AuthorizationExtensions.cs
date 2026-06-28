using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using JacksonVeroneze.NET.GRPCServer.Api.Security;
using JacksonVeroneze.NET.GRPCServer.Infrastructure.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace JacksonVeroneze.NET.GRPCServer.Api.Extensions;

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
            options.AddPolicy(AuthorizationPolicies.JwtAccess, AddJwtRequirements);

            options.AddPolicy(AuthorizationPolicies.ProfilesCreate, policy =>
            {
                AddJwtScopeRequirements(policy, AuthorizationScopes.ProfilesCreate);
            });

            options.AddPolicy(AuthorizationPolicies.ProfilesActivate, policy =>
            {
                AddJwtScopeRequirements(policy, AuthorizationScopes.ProfilesActivate);
            });

            options.AddPolicy(AuthorizationPolicies.ProfilesInactivate, policy =>
            {
                AddJwtScopeRequirements(policy, AuthorizationScopes.ProfilesInactivate);
            });

            options.AddPolicy(AuthorizationPolicies.ProfilesRead, policy =>
            {
                AddJwtScopeRequirements(policy, AuthorizationScopes.ProfilesRead);
            });
        });

        return services;

        static void AddJwtRequirements(AuthorizationPolicyBuilder policy)
        {
            policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
            policy.RequireAuthenticatedUser();
        }

        static void AddJwtScopeRequirements(
            AuthorizationPolicyBuilder policy,
            string requiredScope)
        {
            AddJwtRequirements(policy);
            policy.RequireAssertion(context =>
                HasScope(context.User, requiredScope, "scope", "scp"));
        }

        static bool HasScope(
            ClaimsPrincipal user,
            string requiredScope,
            params string[] claimTypes)
        {
            var scopeClaims = claimTypes
                .SelectMany(claimType => user.FindAll(claimType))
                .SelectMany(claim => claim.Value.Split(
                    ' ',
                    StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));

            return scopeClaims.Contains(requiredScope, StringComparer.Ordinal);
        }
    }
}
