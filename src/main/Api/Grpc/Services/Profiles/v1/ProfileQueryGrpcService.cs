using Grpc.Core;
using JacksonVeroneze.GrpcServer.Contracts.Profiles.V1;
using JacksonVeroneze.NET.GRPCServer.Api.Security;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.GetById;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.GetPaged;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;

namespace JacksonVeroneze.NET.GRPCServer.Api.Grpc.Services.Profiles.v1;

public class ProfileQueryGrpcService(
    IMapper mapper,
    IGetPagedProfilesUseCase getPagedProfilesUseCase,
    IGetByIdProfileUseCase getByIdProfileUseCase)
    : ProfileQueryService.ProfileQueryServiceBase
{
    [Authorize(Policy = AuthorizationPolicies.ProfilesRead)]
    public override async Task<ListProfilesResponse> ListProfiles(
        ListProfilesRequest request, 
        ServerCallContext context)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(context);
        
        var input = mapper.Map<ListProfilesRequest,
            GetPagedProfilesRequest>(request);

        var result = await getPagedProfilesUseCase.ExecuteAsync(
            input, context.CancellationToken);

        var response = mapper.Map<GetPagedProfilesResponse,
            ListProfilesResponse>(result.Value!);

        return response;
    }

    [Authorize(Policy = AuthorizationPolicies.ProfilesRead)]
    public override async Task<GetProfileResponse> GetProfile(
        GetProfileRequest request, 
        ServerCallContext context)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(context);
        
        var input = mapper.Map<GetProfileRequest,
            GetByIdProfileRequest>(request);

        var result = await getByIdProfileUseCase.ExecuteAsync(
            input, context.CancellationToken);
        
        var output = result.ValueOrThrowRpcException();

        var response = mapper.Map<GetByIdProfileResponse,
            GetProfileResponse>(output);

        return response;
    }
}
