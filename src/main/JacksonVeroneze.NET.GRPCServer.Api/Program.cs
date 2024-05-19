using JacksonVeroneze.NET.GRPCServer.Api.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddHealthChecks();

WebApplication app = builder.Build();

app.UseHealthChecks("/health");

app.MapGrpcService<GreeterService>();
app.MapGrpcService<ProductService>();

app.MapGet("/", () => "OK");

app.Run();