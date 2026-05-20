using FluentValidation;
using Grpc.Core;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.List;
using JacksonVeroneze.NET.GRPCServer.Contracts.Profiles.v1;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;

namespace JacksonVeroneze.NET.GRPCServer.Api.Services.v1.Profiles.List;

// [Authorize(Policy = "OrdersList")]
public class ListRequestGrpcService(
    IValidator<ListProfilesRequest> validator,
    Mapper mapper,
    IListProfilesUseCase useCase)
    : ProfileQueryService.ProfileQueryServiceBase
{
    public override async Task<ListProfilesResponse> ListProfiles(
        ListProfilesRequest request,
        ServerCallContext context)
    {
        var validationResult = await validator
            .ValidateAsync(request, context.CancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var input = mapper.Map<ListProfilesRequest,
            ListProfilesQuery>(request);

        var result = await useCase.ExecuteAsync(
            input, context.CancellationToken);

        if (result.IsFailure)
        {
            throw new RpcException(new Status(
                StatusCode.InvalidArgument, result.FirstError!.Message));    
        }
        
        var response = mapper.Map<ListProfilesResult,
            ListProfilesResponse>(result.Value!);

        return response;
    }
}
