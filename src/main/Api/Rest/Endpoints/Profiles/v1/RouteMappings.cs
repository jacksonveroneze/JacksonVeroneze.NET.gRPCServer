using JacksonVeroneze.NET.GRPCServer.Api.Rest.Endpoints.Extensions;

namespace JacksonVeroneze.NET.GRPCServer.Api.Rest.Endpoints.Profiles.v1;

internal static class RouteMappings
{
    private const string Resource = "profiles";
    private const int Version = 1;

    public static WebApplication AddProfilesEndpoints(
        this WebApplication app)
    {
        RouteGroupBuilder builder = RouteGroupBuilderFactory
            .Factory(app, Resource, Version);

        builder.AddCreate()
            .AddGetPaged()
            .AddGetById();

        return app;
    }
}
