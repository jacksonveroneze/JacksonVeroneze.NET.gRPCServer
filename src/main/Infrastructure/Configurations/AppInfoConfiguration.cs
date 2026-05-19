using System.Diagnostics.CodeAnalysis;

namespace JacksonVeroneze.NET.GRPCServer.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record AppInfoConfiguration
{
    public string? Name { get; init; }

    public Version? Version { get; init; }
}
