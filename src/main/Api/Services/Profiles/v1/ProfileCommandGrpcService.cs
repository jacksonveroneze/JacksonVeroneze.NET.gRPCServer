using Grpc.Core;
using JacksonVeroneze.GrpcServer.Contracts.Profiles.V1;
using JacksonVeroneze.NET.GRPCServer.Api.Security;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Activate;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Create;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Inactivate;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using ContractsV1 = JacksonVeroneze.GrpcServer.Contracts.Profiles.V1;
using ContractsApp = JacksonVeroneze.NET.GRPCServer.Application.v1.Profile;

namespace JacksonVeroneze.NET.GRPCServer.Api.Services.Profiles.v1;

public class ProfileCommandGrpcService(
    IMapper mapper,
    ICreateProfileUseCase createProfileUseCase,
    IActivateProfileUseCase activateProfileUseCase,
    IInactivateProfileUseCase inactivateProfileUseCase)
    : ProfileCommandService.ProfileCommandServiceBase
{
    [Authorize(Policy = AuthorizationPolicies.ProfilesCreate)]
    public override async Task<ContractsV1.CreateProfileResponse> CreateProfile(
        ContractsV1.CreateProfileRequest request, 
        ServerCallContext context)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(context);
        
        var input = mapper.Map<ContractsV1.CreateProfileRequest,
            ContractsApp.Create.CreateProfileRequest>(request);

        var result = await createProfileUseCase.ExecuteAsync(
            input, context.CancellationToken);

        result.ThrowRpcExceptionIfFailure();
        
        var response = mapper.Map<ContractsApp.Create.CreateProfileResponse,
            ContractsV1.CreateProfileResponse>(result.Value!);

        return response;
    }

    [Authorize(Policy = AuthorizationPolicies.ProfilesActivate)]
    public override async Task<ActivateProfileResponse> ActivateProfile(
        ContractsV1.ActivateProfileRequest request, 
        ServerCallContext context)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(context);
        
        var input = mapper.Map<ContractsV1.ActivateProfileRequest,
            ContractsApp.Activate.ActivateProfileRequest>(request);

        var result = await activateProfileUseCase.ExecuteAsync(
            input, context.CancellationToken);

        result.ThrowRpcExceptionIfFailure();

        var response = new ActivateProfileResponse();

        return response;
    }

    [Authorize(Policy = AuthorizationPolicies.ProfilesInactivate)]
    public override async Task<InactivateProfileResponse> InactivateProfile(
        ContractsV1.InactivateProfileRequest request, 
        ServerCallContext context)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(context);
        
        var input = mapper.Map<ContractsV1.InactivateProfileRequest,
            ContractsApp.Inactivate.InactivateProfileRequest>(request);

        var result = await inactivateProfileUseCase.ExecuteAsync(
            input, context.CancellationToken);

        result.ThrowRpcExceptionIfFailure();

        var response = new InactivateProfileResponse();

        return response;
    }
}
