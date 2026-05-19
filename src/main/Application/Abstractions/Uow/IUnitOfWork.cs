namespace JacksonVeroneze.NET.GRPCServer.Application.Abstractions.Uow;

public interface IUnitOfWork
{
    public Task<bool> CommitAsync(
        CancellationToken cancellationToken);
}
