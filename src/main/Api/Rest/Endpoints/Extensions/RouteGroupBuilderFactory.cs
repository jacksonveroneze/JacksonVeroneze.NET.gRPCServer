using Asp.Versioning.Builder;

namespace JacksonVeroneze.NET.GRPCServer.Api.Rest.Endpoints.Extensions;

internal static class RouteGroupBuilderFactory
{
    public static RouteGroupBuilder Factory(
        WebApplication app,
        string resource,
        int version,
        bool requireAuthorization = true)
    {
        ApiVersionSet apiVersionSet = app.AddVersion();

        RouteGroupBuilder builder =
            app.MapGroup("/v{version:apiVersion}/" + resource)
                .WithTags(resource)
                .WithApiVersionSet(apiVersionSet)
                .MapToApiVersion(version);

        if (requireAuthorization)
        {
            builder.RequireAuthorization();
        }

        return builder;
    }
}
