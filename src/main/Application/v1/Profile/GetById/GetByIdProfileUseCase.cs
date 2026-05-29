using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.Repositories;
using JacksonVeroneze.NET.GRPCServer.Domain.Errors;
using MapsterMapper;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.GetById;

public class GetByIdProfileUseCase(
    IMapper mapper,
    IProfileRepository repository) : IGetByIdProfileUseCase
{
    public async Task<Result.Result<GetByIdProfileResponse>> ExecuteAsync(
        GetByIdProfileRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var entity = await repository
            .GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result.Result<GetByIdProfileResponse>
                .FromNotFound(DomainErrors.ProfileError.NotFound);
        }

        var response = mapper.Map<Domain.Entities.Profile,
            GetByIdProfileResponse>(entity);

        return Result.Result<GetByIdProfileResponse>
            .WithSuccess(response);
    }
}
