using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.NET.GRPCServer.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class MapperExtensions
{
    public static IServiceCollection AddMapper(
        this IServiceCollection services)
    {
        TypeAdapterConfig config = TypeAdapterConfig.GlobalSettings;

        config.Scan(typeof(AssemblyReference).Assembly);

        services.AddSingleton(config);

        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
