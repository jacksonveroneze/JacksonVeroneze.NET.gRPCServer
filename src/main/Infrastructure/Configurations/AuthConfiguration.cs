using System.Diagnostics.CodeAnalysis;

namespace JacksonVeroneze.NET.GRPCServer.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record AuthConfiguration
{
    public string? Authority { get; init; }

    public string? Audience { get; init; }

    public string? Issuer { get; set; }
}
