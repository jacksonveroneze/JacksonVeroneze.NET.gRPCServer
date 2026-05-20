using FluentValidation;
using Grpc.Core;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Activate;
using JacksonVeroneze.NET.GRPCServer.Contracts.Profiles.v1;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;

namespace JacksonVeroneze.NET.GRPCServer.Api.Services.v1.Profiles.Activate;

// [Authorize(Policy = "ProfileActivate")]
public sealed class ActivateProfileCommandGrpcService(
    IValidator<ActivateProfileRequest> validator,
    IMapper mapper,
    IActivateProfileUseCase useCase)
    : ActivateProfileCommandService.ActivateProfileCommandServiceBase
{
    public override async Task<ActivateProfileResponse> ActivateProfile(
        ActivateProfileRequest request,
        ServerCallContext context)
    {
        var validationResult = await validator
            .ValidateAsync(request, context.CancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var input = mapper.Map<ActivateProfileRequest,
            ActivateProfileCommand>(request);

        var result = await useCase.ExecuteAsync(
            input, context.CancellationToken);

        if (result.IsFailure)
        {
            throw new RpcException(new Status(
                StatusCode.InvalidArgument, result.FirstError!.Message));    
        }

        var response = new ActivateProfileResponse();

        return response;
    }
}
