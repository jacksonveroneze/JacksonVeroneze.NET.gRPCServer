namespace JacksonVeroneze.NET.GRPCServer.Api.Application.Orders.CreateOrder;

public class CreateOrderUseCase(
    IOrderRepository orderRepository)
{
    public async Task<Domain.Orders.Order> ExecuteAsync(
        CreateOrderCommand command,
        CancellationToken cancellationToken)
    {
        List<Domain.Orders.OrderItem> items = command.Items
            .Select(item => new Domain.Orders.OrderItem(
                item.ProductId,
                item.ProductName,
                item.Quantity,
                item.UnitPrice))
            .ToList();

        Domain.Orders.Order order = Domain.Orders.Order.Create(
            command.CustomerId,
            items);

        await orderRepository.AddAsync(order, cancellationToken);

        return order;
    }
}