using System.Diagnostics.CodeAnalysis;

namespace JacksonVeroneze.NET.GRPCServer.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record DatabaseConfiguration
{
    public string? ConnectionString { get; init; }
}
