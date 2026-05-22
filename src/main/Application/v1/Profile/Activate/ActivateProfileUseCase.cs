using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.Repositories;
using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.Services;
using JacksonVeroneze.NET.GRPCServer.Domain.Errors;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Activate;

public class ActivateProfileUseCase(
    IProfileRepository repository,
    IDateTimeProvider dateTime) : IActivateProfileUseCase
{
    public async Task<Result.Result> ExecuteAsync(
        ActivateProfileRequest request, 
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var entity = await repository
            .GetByIdAsync(request.Id, cancellationToken);
        
        if (entity is null)
        {
            return Result.Result.FromNotFound(
                DomainErrors.ProfileError.NotFound);
        }
        
        var result = entity.Activate(dateTime.UtcNow);
        
        if (result.IsFailure)
        {
            return result;
        }
        
        await repository.UpdateAsync(
            entity, cancellationToken);
        
        return Result.Result.WithSuccess();
    }
}
