using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using JacksonVeroneze.NET.GRPCServer.Application;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.NET.GRPCServer.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class MapperExtensions
{
    public static IServiceCollection AddMapper(
        this IServiceCollection services, Assembly assembly)
    {
        TypeAdapterConfig config = TypeAdapterConfig.GlobalSettings;

        config.Scan(typeof(AssemblyReference).Assembly);
        config.Scan(assembly);

        services.AddSingleton(config);

        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
