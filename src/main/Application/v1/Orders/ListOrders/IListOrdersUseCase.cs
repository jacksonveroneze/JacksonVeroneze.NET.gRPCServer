using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.UseCases;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Orders.ListOrders;

public interface IListOrdersUseCase :
    IUseCase<ListOrdersInput, ListOrdersOutput>;
