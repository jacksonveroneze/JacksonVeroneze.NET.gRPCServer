using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.Repositories;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Common.Filters;
using JacksonVeroneze.NET.Pagination.Offset;
using JacksonVeroneze.NET.Result;
using MapsterMapper;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.GetPaged;

public sealed class GetPagedProfilesUseCase(
    IMapper mapper,
    IProfileRepository repository) : IGetPagedProfilesUseCase
{
    public async Task<Result<GetPagedProfilesResponse>> ExecuteAsync(
        GetPagedProfilesRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var filter = mapper.Map<GetPagedProfilesRequest,
            ProfilePagedFilter>(request);

        var page = await repository
            .GetPagedAsync(filter, cancellationToken);

        var response = mapper.Map<Page<Domain.Entities.Profile>,
            GetPagedProfilesResponse>(page);

        return Result<GetPagedProfilesResponse>
            .WithSuccess(response);
    }
}
