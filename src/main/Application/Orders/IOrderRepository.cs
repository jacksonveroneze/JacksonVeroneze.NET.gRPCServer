namespace JacksonVeroneze.NET.GRPCServer.Application.Orders;

public interface IOrderRepository
{
    Task AddAsync(Domain.Orders.Order order, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<Domain.Orders.Order>> ListAsync(
        string? customerId,
        Domain.Orders.OrderStatus? status,
        int pageSize,
        string? pageToken,
        CancellationToken cancellationToken);
}
