using JacksonVeroneze.NET.GRPCServer.Api.Grpc.Services.Profiles.v1;
using JacksonVeroneze.NET.GRPCServer.Api.Security;

namespace JacksonVeroneze.NET.GRPCServer.Api.Grpc.Extensions;

internal static class MapGrpcServices
{
    public static WebApplication AddGrpcServices(
        this WebApplication app)
    {
        app.MapGrpcService<ProfileCommandGrpcService>();
        app.MapGrpcService<ProfileQueryGrpcService>();

        return app;
    }
}
