using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.Repositories;
using JacksonVeroneze.NET.GRPCServer.Domain.Orders;
using JacksonVeroneze.NET.GRPCServer.Infrastructure.Contexts;

namespace JacksonVeroneze.NET.GRPCServer.Infrastructure.Repositories.Order;

[ExcludeFromCodeCoverage]
public class OrderReadRepository
    : IOrderReadRepository
{
    public static readonly ConcurrentDictionary<string, Domain.Orders.Order> Orders = 
        new(StringComparer.OrdinalIgnoreCase);
    
    public Task<IReadOnlyCollection<Domain.Orders.Order>> ListAsync(
        string? customerId, 
        OrderStatus? status, 
        int pageSize, 
        string? pageToken,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        IEnumerable<Domain.Orders.Order> query = Orders.Values.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(customerId))
        {
            query = query.Where(order => order.CustomerId
                .Equals(customerId, StringComparison.OrdinalIgnoreCase));
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
