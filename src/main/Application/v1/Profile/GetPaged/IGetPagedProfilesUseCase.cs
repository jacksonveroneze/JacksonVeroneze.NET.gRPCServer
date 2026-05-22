using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.UseCases;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.GetPaged;

public interface IGetPagedProfilesUseCase :
    IUseCase<GetPagedProfilesRequest, Result.Result<GetPagedProfilesResponse>>;
