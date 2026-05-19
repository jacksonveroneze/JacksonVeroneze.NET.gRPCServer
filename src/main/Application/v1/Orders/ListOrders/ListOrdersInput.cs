using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.UseCases;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Orders.ListOrders;

public sealed record ListOrdersInput(
    string? CustomerId,
    Domain.Orders.OrderStatus? Status,
    int PageSize,
    string? PageToken) : IBaseRequest;
