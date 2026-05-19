using System.Diagnostics.CodeAnalysis;

namespace JacksonVeroneze.NET.GRPCServer.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record DatabaseConfiguration
{
    public string? ReadConnectionString { get; init; }

    public string? WriteConnectionString { get; init; }
}
