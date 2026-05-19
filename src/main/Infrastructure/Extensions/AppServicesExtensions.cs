using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.Repositories;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Orders.CreateOrder;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Orders.ListOrders;
using JacksonVeroneze.NET.GRPCServer.Infrastructure.Repositories.Order;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.NET.GRPCServer.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class AppServicesExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddSingleton<IOrderReadRepository, OrderReadRepository>();
        services.AddSingleton<IOrderRepository, OrderRepository>();
        
        services.AddScoped<ICreateOrderUseCase, CreateOrderUseCase>();
        services.AddScoped<IListOrdersUseCase, ListOrdersUseCase>();

        return services;
    }
}
