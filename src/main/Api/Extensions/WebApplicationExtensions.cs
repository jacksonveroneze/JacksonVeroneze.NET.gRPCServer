using CorrelationId;
using JacksonVeroneze.NET.GRPCServer.Api.Grpc.Extensions;
using JacksonVeroneze.NET.GRPCServer.Api.Rest.Endpoints.Profiles.v1;
using JacksonVeroneze.NET.GRPCServer.Api.Security;

namespace JacksonVeroneze.NET.GRPCServer.Api.Extensions;

internal static class WebApplicationExtensions
{
    private const string PathHealth = "/health";
    private const string PathMetrics = "metrics";
    private const string PathMcp = "mcp";
    
    public static WebApplication Configure(
        this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        app.UseCorrelationId();
        
        if (app.Environment.IsDevelopment())
        {
            app.MapGrpcReflectionService();
        }

        app.UseRouting();

        app.UseHealthChecks(PathHealth);
        app.UseOpenTelemetryPrometheusScrapingEndpoint(PathMetrics);

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapMcp(PathMcp)
            .RequireAuthorization(AuthorizationPolicies.McpAccess);

        app.AddGrpcServices();
        app.AddProfilesEndpoints();
        
        return app;
    }
}
