using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.GRPCServer.Domain.Enums;
using JacksonVeroneze.NET.Pagination.Offset;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Common.Filters;

[ExcludeFromCodeCoverage]
public sealed record ProfilePagedFilter
{
    public string? FullName { get; init; }

    public Gender? Gender { get; init; }

    public string? Cpf { get; init; }

    public ProfileStatus? Status { get; init; }

    public PaginationParameters? Pagination { get; init; }
}
