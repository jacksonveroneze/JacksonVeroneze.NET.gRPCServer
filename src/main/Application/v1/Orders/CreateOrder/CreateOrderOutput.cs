using JacksonVeroneze.NET.GRPCServer.Application.Common.Models.Common.Response;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Orders.CreateOrder;

public sealed record CreateOrderOutput
    : DataResponse<Guid>;
