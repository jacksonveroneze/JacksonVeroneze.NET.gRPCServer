using JacksonVeroneze.NET.GRPCServer.Api.Services.Orders.v1;
using JacksonVeroneze.NET.GRPCServer.Application.Orders;
using JacksonVeroneze.NET.GRPCServer.Application.Orders.CreateOrder;
using JacksonVeroneze.NET.GRPCServer.Application.Orders.ListOrders;
using JacksonVeroneze.NET.GRPCServer.Infrastructure;

WebApplicationBuilder builder =
    WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddHealthChecks();

builder.Services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();
builder.Services.AddScoped<CreateOrderUseCase>();
builder.Services.AddScoped<ListOrdersUseCase>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

app.MapGrpcService<OrderQueryGrpcService>();
app.MapGrpcService<OrderCommandGrpcService>();

app.UseHealthChecks("/health");
app.MapGet("/", () => "OK");

await app.RunAsync();
