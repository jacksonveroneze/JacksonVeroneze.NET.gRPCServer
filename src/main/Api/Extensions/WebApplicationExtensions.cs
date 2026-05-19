using JacksonVeroneze.NET.GRPCServer.Api.Services.Orders.v1;

namespace JacksonVeroneze.NET.GRPCServer.Api.Extensions;

internal static class WebApplicationExtensions
{
    public static WebApplication Configure(
        this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        app.UseRequestLocalization();

        if (app.Environment.IsDevelopment())
        {
            app.MapGrpcReflectionService();
        }

        app.UseRouting();

        app.UseHealthChecks("/health");

        app.UseOpenTelemetryPrometheusScrapingEndpoint("metrics-open");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapGrpcService<OrderQueryGrpcService>();
        app.MapGrpcService<OrderCommandGrpcService>();
        
        app.MapGet("/", () => "OK");

        return app;
    }
}
