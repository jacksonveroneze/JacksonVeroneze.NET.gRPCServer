using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using JacksonVeroneze.NET.GRPCServer.Infrastructure.Configurations;
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
            options.AddPolicy("OrdersCreate", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireAssertion(context =>
                    HasScope(context.User, "orders.create"));
            });

            options.AddPolicy("OrdersList", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireAssertion(context =>
                    HasScope(context.User, "orders.list"));
            });
        });

        static bool HasScope(ClaimsPrincipal user, string requiredScope)
        {
            var scopeClaims = user.FindAll("scope")
                .Concat(user.FindAll("scp"))
                .SelectMany(claim => claim.Value.Split(' ', StringSplitOptions.RemoveEmptyEntries));

            return scopeClaims.Contains(requiredScope, StringComparer.Ordinal);
        }

        return services;
    }
}
