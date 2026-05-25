using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace JacksonVeroneze.NET.GRPCServer.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record DatabaseConfiguration
{
    [Required]
    public string? ConnectionString { get; init; }
}
