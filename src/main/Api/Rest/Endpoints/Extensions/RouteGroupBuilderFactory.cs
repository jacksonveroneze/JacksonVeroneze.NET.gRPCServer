using Asp.Versioning.Builder;
using JacksonVeroneze.NET.GRPCServer.Api.Security;

namespace JacksonVeroneze.NET.GRPCServer.Api.Rest.Endpoints.Extensions;

internal static class RouteGroupBuilderFactory
{
    public static RouteGroupBuilder Factory(
        WebApplication app,
        string resource,
        int version)
    {
        ApiVersionSet apiVersionSet = app.AddVersion();

        RouteGroupBuilder builder =
            app.MapGroup("/v{version:apiVersion}/" + resource)
                .WithTags(resource)
                .WithApiVersionSet(apiVersionSet)
                .MapToApiVersion(version)
                .RequireAuthorization(AuthorizationPolicies.JwtAccess);

        return builder;
    }
}
