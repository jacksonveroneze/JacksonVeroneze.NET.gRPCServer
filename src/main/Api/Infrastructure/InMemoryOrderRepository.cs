using System.Collections.Concurrent;
using JacksonVeroneze.NET.GRPCServer.Api.Application.Orders;

namespace JacksonVeroneze.NET.GRPCServer.Api.Infrastructure;

public sealed class InMemoryOrderRepository : IOrderRepository
{
    private readonly ConcurrentDictionary<string, Domain.Orders.Order> _orders = 
        new(StringComparer.OrdinalIgnoreCase);

    public Task AddAsync(Domain.Orders.Order order, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _orders.TryAdd(order.OrderId, order);

        return Task.CompletedTask;
    }

    public Task<IReadOnlyCollection<Domain.Orders.Order>> ListAsync(
        string? customerId,
        Domain.Orders.OrderStatus? status,
        int pageSize,
        string? pageToken,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        IEnumerable<Domain.Orders.Order> query = _orders.Values.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(customerId))
        {
            query = query.Where(order => order.CustomerId.Equals(customerId, StringComparison.OrdinalIgnoreCase));
        }

        if (status is not null)
        {
            query = query.Where(order => order.Status == status);
        }

        List<Domain.Orders.Order> orders = query
            .OrderByDescending(order => order.CreatedAt)
            .Take(pageSize)
            .ToList();

        return Task.FromResult<IReadOnlyCollection<Domain.Orders.Order>>(orders);
    }
}
