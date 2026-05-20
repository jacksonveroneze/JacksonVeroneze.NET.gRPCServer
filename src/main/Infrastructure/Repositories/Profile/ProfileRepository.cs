using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.Repositories;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Common.Filters;
using JacksonVeroneze.NET.GRPCServer.Infrastructure.Builders.Filters;
using JacksonVeroneze.NET.GRPCServer.Infrastructure.Contexts;
using JacksonVeroneze.NET.Pagination.Cursor;
using JacksonVeroneze.NET.Pagination.Cursor.Extensions;

namespace JacksonVeroneze.NET.GRPCServer.Infrastructure.Repositories.Profile;

[ExcludeFromCodeCoverage]
public class ProfileRepository(
    IEfCoreRepository<Domain.Entities.Profile, DefaultDbContext> service)
    : IProfileRepository
{
    #region read

    public async Task<Domain.Entities.Profile?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        Domain.Entities.Profile? result = await service.GetByIdAsync(
            conf => conf.Id == id,
            cancellationToken);

        return result;
    }

    public async Task<Page<Domain.Entities.Profile>> GetPagedAsync(
        ProfilePagedFilter filter,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(filter);

        Expression<Func<Domain.Entities.Profile, bool>> expression =
            ProfilePagedFilterBuilder.Create(filter).Build();

        int pageLimit = filter.Pagination!.Limit!.Value;
        int databaselimit = pageLimit + 1;

        ICollection<Domain.Entities.Profile> result =
            await service.GetAllAsync(
                expression,
                order => order.Id,
                databaselimit,
                cancellationToken: cancellationToken);

        return result.ToPage(
            filter.Pagination, d => d.Id.ToString());
    }

    public async Task<bool> ExistsByCpfAsync(
        string cpf,
        CancellationToken cancellationToken)
    {
        Expression<Func<Domain.Entities.Profile, bool>> spec
            = entity => entity.Cpf == cpf;

        bool exists = await service
            .AnyAsync(spec, cancellationToken);

        return exists;
    }

    #endregion

    #region write

    public async Task CreateAsync(
        Domain.Entities.Profile entity,
        CancellationToken cancellationToken)
    {
        await service.CreateAsync(entity, cancellationToken);
        
        await service.DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(
        Domain.Entities.Profile entity,
        CancellationToken cancellationToken)
    {
        service.Delete(entity);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        Domain.Entities.Profile entity,
        CancellationToken cancellationToken)
    {
        service.Update(entity);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }

    #endregion
}
