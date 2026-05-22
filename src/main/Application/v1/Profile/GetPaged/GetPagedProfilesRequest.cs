using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.UseCases;
using JacksonVeroneze.NET.GRPCServer.Application.Common.Models.Common.Request;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Common.Models;
using JacksonVeroneze.NET.GRPCServer.Domain.Enums;
using JacksonVeroneze.NET.Pagination.Enums;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.GetPaged;

public sealed record GetPagedProfilesRequest()
    : PagedRequest(DefaultOrderBy, DefaultOrder),
        IBaseRequest
{
    private const string DefaultOrderBy = nameof(ProfileResponse.FullName);

    private const SortDirection DefaultOrder = SortDirection.Ascending;
    
    public string? FullName { get; init; }

    public Gender? Gender { get; init; }

    public string? Cpf { get; init; }

    public ProfileStatus? Status { get; init; }
}
