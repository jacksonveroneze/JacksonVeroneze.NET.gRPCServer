using JacksonVeroneze.NET.GRPCServer.Api.Services.v1.Profiles.Activate;
using JacksonVeroneze.NET.GRPCServer.Api.Services.v1.Profiles.Create;
using JacksonVeroneze.NET.GRPCServer.Api.Services.v1.Profiles.Inactivate;
using JacksonVeroneze.NET.GRPCServer.Api.Services.v1.Profiles.List;

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

        app.MapGrpcService<ListRequestGrpcService>();
        app.MapGrpcService<CreateProfileCommandGrpcService>();
        app.MapGrpcService<ActivateProfileCommandGrpcService>();
        app.MapGrpcService<InactivateProfileCommandGrpcService>();
        
        app.MapGet("/", () => "OK");

        return app;
    }
}
