using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.Repositories;
using JacksonVeroneze.NET.Result;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Orders.CreateOrder;

public class CreateOrderUseCase(
    IOrderRepository orderRepository) : ICreateOrderUseCase
{
    public async Task<Result<CreateOrderOutput>> ExecuteAsync(
        CreateOrderInput input, 
        CancellationToken cancellationToken)
    {
        List<Domain.Orders.OrderItem> items = input.Items
            .Select(item => new Domain.Orders.OrderItem(
                item.ProductId,
                item.ProductName,
                item.Quantity,
                item.UnitPrice))
            .ToList();
        
        Domain.Orders.Order order = Domain.Orders.Order.Create(
            input.CustomerId,
            items);
        
        await orderRepository.AddAsync(order, cancellationToken);

        CreateOrderOutput result = new();
        
        return Result<CreateOrderOutput>.WithSuccess(result);
    }
}
