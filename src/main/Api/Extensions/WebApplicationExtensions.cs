using CorrelationId;
using JacksonVeroneze.NET.GRPCServer.Api.Grpc.Services.Profiles.v1;
using JacksonVeroneze.NET.GRPCServer.Api.Rest.Endpoints.Profiles.v1;

namespace JacksonVeroneze.NET.GRPCServer.Api.Extensions;

internal static class WebApplicationExtensions
{
    public static WebApplication Configure(
        this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        app.UseCorrelationId();
        
        app.UseRequestLocalization();

        if (app.Environment.IsDevelopment())
        {
            app.MapGrpcReflectionService();
        }

        app.UseRouting();

        app.UseHealthChecks("/health");
        app.UseOpenTelemetryPrometheusScrapingEndpoint("metrics");
        
        app.MapMcp("mcp");

        app.UseAuthentication();
        app.UseAuthorization();

        app.AddProfilesEndpoints();
        
        app.MapGrpcService<ProfileCommandGrpcService>();
        app.MapGrpcService<ProfileQueryGrpcService>();

        return app;
    }
}
