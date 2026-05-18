namespace JacksonVeroneze.NET.GRPCServer.Api.Domain.Orders;

public enum OrderStatus
{
    None = 0,
    Pending = 1,
    Confirmed = 2,
    Cancelled = 3,
    Rejected = 4
}