using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.UseCases;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Orders.CreateOrder;

public sealed record CreateOrderInput(
    string CustomerId,
    IReadOnlyCollection<CreateOrderItemInput> Items)
    : IBaseRequest;

public sealed record CreateOrderItemInput(
    string ProductId,
    string ProductName,
    int Quantity,
    Domain.Orders.Money UnitPrice);
