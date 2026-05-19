namespace JacksonVeroneze.NET.GRPCServer.Application.Abstractions.Repositories;

public interface IOrderRepository
{
    Task AddAsync(
        Domain.Orders.Order order, 
        CancellationToken cancellationToken);
}
