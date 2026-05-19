namespace JacksonVeroneze.NET.GRPCServer.Application.Abstractions.Repositories;

public interface IOrderReadRepository
{
    Task<IReadOnlyCollection<Domain.Orders.Order>> ListAsync(
        string? customerId,
        Domain.Orders.OrderStatus? status,
        int pageSize,
        string? pageToken,
        CancellationToken cancellationToken);
}
