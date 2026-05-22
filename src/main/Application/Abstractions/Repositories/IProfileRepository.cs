using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Common.Filters;
using JacksonVeroneze.NET.GRPCServer.Domain.Entities;
using JacksonVeroneze.NET.Pagination.Cursor;

namespace JacksonVeroneze.NET.GRPCServer.Application.Abstractions.Repositories;

public interface IProfileRepository
{
    Task<Profile?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<Page<Profile>> GetPagedAsync(
        ProfilePagedFilter filter,
        CancellationToken cancellationToken);

    Task<bool> ExistsByCpfAsync(
        string cpf,
        CancellationToken cancellationToken);

    Task CreateAsync(
        Profile entity,
        CancellationToken cancellationToken);

    Task DeleteAsync(
        Profile entity,
        CancellationToken cancellationToken);

    Task UpdateAsync(
        Profile entity,
        CancellationToken cancellationToken);
}
