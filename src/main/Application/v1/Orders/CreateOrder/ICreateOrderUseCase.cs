using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.UseCases;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Orders.CreateOrder;

public interface ICreateOrderUseCase :
    IUseCase<CreateOrderInput, CreateOrderOutput>;
