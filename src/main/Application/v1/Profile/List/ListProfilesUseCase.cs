using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.Repositories;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Common.Filters;
using JacksonVeroneze.NET.Pagination.Cursor;
using JacksonVeroneze.NET.Result;
using MapsterMapper;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.List;

public sealed class ListProfilesUseCase(
    IMapper mapper,
    IProfileRepository repository) : IListProfilesUseCase
{
    public async Task<Result<ListProfilesResult>> ExecuteAsync(
        ListProfilesQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var filter = mapper.Map<ListProfilesQuery,
            ProfilePagedFilter>(request);

        Page<Domain.Entities.Profile> page = await repository
            .GetPagedAsync(filter, cancellationToken);

        var response = mapper.Map<Page<Domain.Entities.Profile>,
            ListProfilesResult>(page);

        return Result<ListProfilesResult>
            .WithSuccess(response);
    }
}
