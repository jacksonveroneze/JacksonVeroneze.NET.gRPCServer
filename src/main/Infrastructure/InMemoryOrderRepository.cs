using System.Collections.Concurrent;
using JacksonVeroneze.NET.GRPCServer.Application.Orders;
using JacksonVeroneze.NET.GRPCServer.Domain.Orders;

namespace JacksonVeroneze.NET.GRPCServer.Infrastructure;

public sealed class InMemoryOrderRepository : IOrderRepository
{
    private readonly ConcurrentDictionary<string, Order> _orders = 
        new(StringComparer.OrdinalIgnoreCase);

    public Task AddAsync(Order order, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _orders.TryAdd(order.OrderId, order);

        return Task.CompletedTask;
    }

    public Task<IReadOnlyCollection<Order>> ListAsync(
        string? customerId,
        OrderStatus? status,
        int pageSize,
        string? pageToken,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        IEnumerable<Order> query = _orders.Values.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(customerId))
        {
            query = query.Where(order => order.CustomerId.Equals(customerId, StringComparison.OrdinalIgnoreCase));
        }

        if (status is not null)
        {
            query = query.Where(order => order.Status == status);
        }

        List<Order> orders = query
            .OrderByDescending(order => order.CreatedAt)
            .Take(pageSize)
            .ToList();

        return Task.FromResult<IReadOnlyCollection<Order>>(orders);
    }
}
