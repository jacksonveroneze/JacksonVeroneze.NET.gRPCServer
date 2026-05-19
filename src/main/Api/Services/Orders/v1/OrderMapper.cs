using Google.Protobuf.WellKnownTypes;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Orders.CreateOrder;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Orders.ListOrders;
using JacksonVeroneze.NET.GRPCServer.Contracts.Orders.v1;

namespace JacksonVeroneze.NET.GRPCServer.Api.Services.Orders.v1;

public static class OrderMapper
{
    public static CreateOrderInput ToCommand(CreateOrderRequest request)
    {
        return new CreateOrderInput(
            CustomerId: request.CustomerId,
            Items: request.Items
                .Select(item => new CreateOrderItemInput(
                    ProductId: item.ProductId,
                    ProductName: item.ProductName,
                    Quantity: item.Quantity,
                    UnitPrice: ToDomainMoney(item.UnitPrice)))
                .ToList());
    }

    public static ListOrdersInput ToQuery(ListOrdersRequest request)
    {
        return new ListOrdersInput(
            CustomerId: string.IsNullOrWhiteSpace(request.CustomerId) ? null : request.CustomerId,
            Status: ToDomainStatusOrNull(request.Status),
            PageSize: request.PageSize,
            PageToken: string.IsNullOrWhiteSpace(request.PageToken) ? null : request.PageToken);
    }

    public static Order ToGrpcOrder(Domain.Orders.Order order)
    {
        Order grpcOrder = new()
        {
            OrderId = order.OrderId,
            CustomerId = order.CustomerId,
            Status = ToGrpcStatus(order.Status),
            TotalAmount = ToGrpcMoney(order.TotalAmount),
            CreatedAt = Timestamp.FromDateTimeOffset(order.CreatedAt)
        };

        grpcOrder.Items.AddRange(order.Items.Select(ToGrpcOrderItem));

        return grpcOrder;
    }

    private static OrderItem ToGrpcOrderItem(Domain.Orders.OrderItem item)
    {
        return new OrderItem
        {
            ProductId = item.ProductId,
            ProductName = item.ProductName,
            Quantity = item.Quantity,
            UnitPrice = ToGrpcMoney(item.UnitPrice),
            TotalPrice = ToGrpcMoney(item.TotalPrice)
        };
    }

    public static OrderSummary ToGrpcOrderSummary(Domain.Orders.Order order)
    {
        return new OrderSummary
        {
            OrderId = order.OrderId,
            CustomerId = order.CustomerId,
            Status = ToGrpcStatus(order.Status),
            TotalAmount = ToGrpcMoney(order.TotalAmount),
            CreatedAt = Timestamp.FromDateTimeOffset(order.CreatedAt)
        };
    }

    private static Domain.Orders.Money ToDomainMoney(Money money)
    {
        return new Domain.Orders.Money(
            money.CurrencyCode,
            money.Units,
            money.Nanos);
    }

    private static Money ToGrpcMoney(Domain.Orders.Money money)
    {
        return new Money
        {
            CurrencyCode = money.CurrencyCode,
            Units = money.Units,
            Nanos = money.Nanos
        };
    }

    private static Domain.Orders.OrderStatus? ToDomainStatusOrNull(OrderStatus status)
    {
        return status switch
        {
            OrderStatus.Unspecified => null,
            OrderStatus.Pending => Domain.Orders.OrderStatus.Pending,
            OrderStatus.Confirmed => Domain.Orders.OrderStatus.Confirmed,
            OrderStatus.Cancelled => Domain.Orders.OrderStatus.Cancelled,
            OrderStatus.Rejected => Domain.Orders.OrderStatus.Rejected,
            _ => null
        };
    }

    private static OrderStatus ToGrpcStatus(Domain.Orders.OrderStatus status)
    {
        return status switch
        {
            Domain.Orders.OrderStatus.Pending => OrderStatus.Pending,
            Domain.Orders.OrderStatus.Confirmed => OrderStatus.Confirmed,
            Domain.Orders.OrderStatus.Cancelled => OrderStatus.Cancelled,
            Domain.Orders.OrderStatus.Rejected => OrderStatus.Rejected,
            _ => OrderStatus.Unspecified
        };
    }
}
