using FluentValidation;
using Grpc.Core;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Create;
using JacksonVeroneze.NET.GRPCServer.Contracts.Profiles.v1;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;

namespace JacksonVeroneze.NET.GRPCServer.Api.Services.v1.Profiles.Create;

// [Authorize(Policy = "ProfileCreate")]
public sealed class CreateProfileCommandGrpcService(
    IValidator<CreateProfileRequest> validator,
    IMapper mapper,
    ICreateProfileUseCase useCase)
    : ProfileCommandService.ProfileCommandServiceBase
{
    public override async Task<CreateProfileResponse> CreateProfile(
        CreateProfileRequest request,
        ServerCallContext context)
    {
        var validationResult = await validator
            .ValidateAsync(request, context.CancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var input = mapper.Map<CreateProfileRequest,
            CreateProfileCommand>(request);

        var result = await useCase.ExecuteAsync(
            input, context.CancellationToken);

        if (result.IsFailure)
        {
            throw new RpcException(new Status(
                StatusCode.InvalidArgument, result.FirstError!.Message));    
        }
        
        var response = mapper.Map<CreateProfileResult,
            CreateProfileResponse>(result.Value!);

        return response;
    }
}
