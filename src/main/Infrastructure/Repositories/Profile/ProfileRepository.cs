using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.Repositories;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Common.Filters;
using JacksonVeroneze.NET.GRPCServer.Infrastructure.Builders.Filters;
using JacksonVeroneze.NET.GRPCServer.Infrastructure.Contexts;
using JacksonVeroneze.NET.Pagination.Offset;

namespace JacksonVeroneze.NET.GRPCServer.Infrastructure.Repositories.Profile;

[ExcludeFromCodeCoverage]
public class ProfileRepository(
    IEfCoreRepository<Domain.Entities.Profile, DefaultDbContext> efRepository)
    : IProfileRepository
{
    #region read

    public async Task<Domain.Entities.Profile?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await efRepository.GetByIdAsync(
            conf => conf.Id == id,
            cancellationToken);

        return result;
    }

    public Task<Page<Domain.Entities.Profile>> GetPagedAsync(
        ProfilePagedFilter filter,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(filter);

        var expression = ProfilePagedFilterBuilder
            .Create(filter).Build();

        var result = efRepository.GetPagedAsync(
            filter.Pagination!,
            expression,
            order => order.Id,
            cancellationToken: cancellationToken);

        return result;
    }

    public async Task<bool> ExistsByCpfAsync(
        string cpf,
        CancellationToken cancellationToken)
    {
        Expression<Func<Domain.Entities.Profile, bool>> spec
            = entity => entity.Cpf == cpf;

        var exists = await efRepository
            .AnyAsync(spec, cancellationToken);

        return exists;
    }

    #endregion

    #region write

    public async Task CreateAsync(
        Domain.Entities.Profile entity,
        CancellationToken cancellationToken)
    {
        await efRepository.CreateAsync(entity, cancellationToken);

        await efRepository.DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(
        Domain.Entities.Profile entity,
        CancellationToken cancellationToken)
    {
        efRepository.Delete(entity);

        await efRepository.DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        Domain.Entities.Profile entity,
        CancellationToken cancellationToken)
    {
        efRepository.Update(entity);

        await efRepository.DbContext.SaveChangesAsync(cancellationToken);
    }

    #endregion
}
