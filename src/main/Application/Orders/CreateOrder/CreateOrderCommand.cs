namespace JacksonVeroneze.NET.GRPCServer.Application.Orders.CreateOrder;

public sealed record CreateOrderCommand(
    string CustomerId,
    IReadOnlyCollection<CreateOrderCommandItem> Items);

public sealed record CreateOrderCommandItem(
    string ProductId,
    string ProductName,
    int Quantity,
    Domain.Orders.Money UnitPrice);
