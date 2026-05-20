using FluentValidation;
using Grpc.Core;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Inactivate;
using JacksonVeroneze.NET.GRPCServer.Contracts.Profiles.v1;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;

namespace JacksonVeroneze.NET.GRPCServer.Api.Services.v1.Profiles.Inactivate;

// [Authorize(Policy = "ProfileInactivate")]
public sealed class InactivateProfileCommandGrpcService(
    IValidator<InactivateProfileRequest> validator,
    IMapper mapper,
    IInactivateProfileUseCase useCase)
    : InactivateProfileCommandService.InactivateProfileCommandServiceBase
{
    public override async Task<InactivateProfileResponse> InactivateProfile(
        InactivateProfileRequest request,
        ServerCallContext context)
    {
        var validationResult = await validator
            .ValidateAsync(request, context.CancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var input = mapper.Map<InactivateProfileRequest,
            InactivateProfileCommand>(request);

        var result = await useCase.ExecuteAsync(
            input, context.CancellationToken);

        if (result.IsFailure)
        {
            throw new RpcException(new Status(
                StatusCode.InvalidArgument, result.FirstError!.Message));    
        }
        
        var response = new InactivateProfileResponse();

        return response;
    }
}
