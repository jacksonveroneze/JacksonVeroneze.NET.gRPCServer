using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.GRPCServer.Infrastructure.Configurations;

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
                configureOption.ServerInfo!.Name = appConfiguration.AppName;
                configureOption.ServerInfo!.Version = appConfiguration.AppVersion.ToString();
            })
            //.WithStdioServerTransport()
            .WithHttpTransport(options => { options.Stateless = true; })
            .WithToolsFromAssembly(AssemblyReference.Assembly);

        return services;
    }
}
