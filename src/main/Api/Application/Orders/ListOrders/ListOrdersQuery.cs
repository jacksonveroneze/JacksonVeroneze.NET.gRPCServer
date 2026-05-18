namespace JacksonVeroneze.NET.GRPCServer.Api.Application.Orders.ListOrders;

public sealed record ListOrdersQuery(
    string? CustomerId,
    Domain.Orders.OrderStatus? Status,
    int PageSize,
    string? PageToken);