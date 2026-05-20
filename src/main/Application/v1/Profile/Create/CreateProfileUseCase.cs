using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.Repositories;
using JacksonVeroneze.NET.GRPCServer.Domain.Errors;
using JacksonVeroneze.NET.Result;
using MapsterMapper;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Create;

public class CreateProfileUseCase(
    IMapper mapper,
    IProfileRepository repository) : ICreateProfileUseCase
{
    public async Task<Result<CreateProfileResult>> ExecuteAsync(
        CreateProfileCommand request, 
        CancellationToken cancellationToken)
    {
        var alreadyExists = await repository.ExistsByCpfAsync(
            request.Cpf!, cancellationToken);
        
        if (alreadyExists)
        {
            Error error = DomainErrors.ProfileError.Duplicated;

            return Result<CreateProfileResult>.FromConflict(error);
        }
        
        var profile = new Domain.Entities.Profile(
            name: request.Name,
            birthday: request.Birthday,
            gender: request.Gender,
            cpf: request.Cpf);

        await repository.CreateAsync(profile, cancellationToken);

        var result = mapper.Map<Domain.Entities.Profile, 
            CreateProfileResult>(profile);
        
        return Result<CreateProfileResult>.WithSuccess(result);
    }
}
