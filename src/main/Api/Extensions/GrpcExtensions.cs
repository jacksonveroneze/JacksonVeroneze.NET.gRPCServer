using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.GRPCServer.Api.Grpc.Interceptors;

namespace JacksonVeroneze.NET.GRPCServer.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class GrpcExtensions
{
    public static IServiceCollection AddGrpc(
        this IServiceCollection services,
        bool enableDetailedErrors)
    {
        services.AddGrpc(options =>
        {
            options.EnableDetailedErrors = enableDetailedErrors;
            options.Interceptors.Add<GrpcExceptionInterceptor>();
            options.Interceptors.Add<GrpcValidationInterceptor>();
        });
        
        return services;
    }
}
