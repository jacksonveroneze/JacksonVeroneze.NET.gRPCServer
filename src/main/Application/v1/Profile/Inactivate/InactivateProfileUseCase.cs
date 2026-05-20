using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.Repositories;
using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.Services;
using JacksonVeroneze.NET.GRPCServer.Domain.Errors;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Inactivate;

public class InactivateProfileUseCase(
    IProfileRepository repository,
    IDateTimeProvider dateTime) : IInactivateProfileUseCase
{
    public async Task<Result.Result> ExecuteAsync(
        InactivateProfileCommand request, 
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        Domain.Entities.Profile? entity = await repository
            .GetByIdAsync(request.Id, cancellationToken);
        
        if (entity is null)
        {
            return Result.Result.FromNotFound(
                DomainErrors.ProfileError.NotFound);
        }
        
        Result.Result result = entity.Inactivate(dateTime.UtcNow);
        
        if (result.IsFailure)
        {
            return result;
        }
        
        await repository.UpdateAsync(
            entity, cancellationToken);
        
        return Result.Result.WithSuccess();
    }
}
