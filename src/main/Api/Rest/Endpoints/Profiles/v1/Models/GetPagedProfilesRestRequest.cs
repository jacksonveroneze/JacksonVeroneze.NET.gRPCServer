using JacksonVeroneze.NET.GRPCServer.Domain.Enums;
using JacksonVeroneze.NET.Pagination.Enums;

namespace JacksonVeroneze.NET.GRPCServer.Api.Rest.Endpoints.Profiles.v1.Models;

internal sealed record GetPagedProfilesRestRequest
{
    public string? FullName { get; init; }

    public Gender? Gender { get; init; }

    public string? Cpf { get; init; }

    public ProfileStatus? Status { get; init; }

    public int? Page { get; init; }

    public int? PageSize { get; init; }

    public string? OrderBy { get; init; }

    public SortDirection? Order { get; init; }
}
