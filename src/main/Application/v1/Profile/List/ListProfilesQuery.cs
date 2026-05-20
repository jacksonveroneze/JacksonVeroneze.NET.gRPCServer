using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.UseCases;
using JacksonVeroneze.NET.GRPCServer.Application.Common.Models.Common.Queries;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Common.Models;
using JacksonVeroneze.NET.Pagination.Enums;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.List;

public sealed record ListProfilesQuery()
    : PagedCursorQuery(DefaultOrderBy, DefaultOrder),
        IBaseRequest
{
    private const string DefaultOrderBy = nameof(ProfileResult.Name);

    private const SortDirection DefaultOrder = SortDirection.Ascending;
}
