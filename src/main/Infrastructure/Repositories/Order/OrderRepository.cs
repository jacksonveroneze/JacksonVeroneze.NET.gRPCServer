using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.Repositories;
using JacksonVeroneze.NET.GRPCServer.Infrastructure.Contexts;

namespace JacksonVeroneze.NET.GRPCServer.Infrastructure.Repositories.Order;

[ExcludeFromCodeCoverage]
public class OrderRepository
    : IOrderRepository
{
    public Task AddAsync(
        Domain.Orders.Order order, 
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        OrderReadRepository.Orders.TryAdd(order.OrderId, order);

        return Task.CompletedTask;
    }
}
