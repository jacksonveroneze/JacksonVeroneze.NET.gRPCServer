using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.Pagination.Cursor;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Common.Filters;

[ExcludeFromCodeCoverage]
public record ProfilePagedFilter
{
    public string? Cursor { get; init; }

    public string? Name { get; init; }

    public PaginationParameters? Pagination { get; init; }
}
