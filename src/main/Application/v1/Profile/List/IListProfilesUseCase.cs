using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.UseCases;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.List;

public interface IListProfilesUseCase :
    IUseCase<ListProfilesQuery, Result.Result<ListProfilesResult>>;
