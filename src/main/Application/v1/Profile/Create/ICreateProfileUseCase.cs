using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.UseCases;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Create;

public interface ICreateProfileUseCase :
    IUseCase<CreateProfileRequest, Result.Result<CreateProfileResponse>>;
