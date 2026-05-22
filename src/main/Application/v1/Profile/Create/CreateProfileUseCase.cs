using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.Repositories;
using JacksonVeroneze.NET.GRPCServer.Domain.Errors;
using JacksonVeroneze.NET.Result;
using MapsterMapper;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Create;

public class CreateProfileUseCase(
    IMapper mapper,
    IProfileRepository repository) : ICreateProfileUseCase
{
    public async Task<Result<CreateProfileResponse>> ExecuteAsync(
        CreateProfileRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var alreadyExists = await repository
            .ExistsByCpfAsync(request.Cpf, cancellationToken);

        if (alreadyExists)
        {
            var error = DomainErrors.ProfileError.Duplicated;

            return Result<CreateProfileResponse>.FromConflict(error);
        }

        var entity = mapper.Map<CreateProfileRequest,
            Domain.Entities.Profile>(request);

        await repository.CreateAsync(entity, cancellationToken);

        var result = mapper.Map<Domain.Entities.Profile,
            CreateProfileResponse>(entity);

        return Result<CreateProfileResponse>.WithSuccess(result);
    }
}
