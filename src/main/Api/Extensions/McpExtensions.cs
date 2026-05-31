using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.GRPCServer.Infrastructure.Configurations;
using ModelContextProtocol.Protocol;

namespace JacksonVeroneze.NET.GRPCServer.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class McpExtensions
{
    public static IServiceCollection AddMcp(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        services.AddMcpServer(configureOption =>
            {
                configureOption.ServerInfo = new Implementation
                {
                    Name = appConfiguration.AppName,
                    Version = appConfiguration.AppVersion.ToString(),
                };
            })
            .WithHttpTransport(options => { options.Stateless = true; })
            .WithToolsFromAssembly(AssemblyReference.Assembly);

        return services;
    }
}
