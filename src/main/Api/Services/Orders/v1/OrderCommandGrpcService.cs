using Grpc.Core;
using JacksonVeroneze.NET.GRPCServer.Application.Orders.CreateOrder;
using JacksonVeroneze.NET.GRPCServer.Contracts.Orders.v1;

namespace JacksonVeroneze.NET.GRPCServer.Api.Services.Orders.v1;

public sealed class OrderCommandGrpcService(
    CreateOrderUseCase useCase,
    ILogger<OrderCommandGrpcService> logger)
    : OrderCommandService.OrderCommandServiceBase
{

    public override async Task<CreateOrderResponse> CreateOrder(
        CreateOrderRequest request,
        ServerCallContext context)
    {
        try
        {
            ValidateCreateOrderRequest(request);

            CreateOrderCommand command = OrderMapper.ToCommand(request);

            Domain.Orders.Order order = await useCase.ExecuteAsync(
                command,
                context.CancellationToken);

            return new CreateOrderResponse
            {
                Order = OrderMapper.ToGrpcOrder(order)
            };
        }
        catch (RpcException)
        {
            throw;
        }
        catch (OperationCanceledException) when (context.CancellationToken.IsCancellationRequested)
        {
            logger.LogWarning("CreateOrder call was cancelled by the client.");

            throw new RpcException(new Status(
                StatusCode.Cancelled,
                "The operation was cancelled."));
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(exception, "Invalid CreateOrder request.");

            throw new RpcException(new Status(
                StatusCode.InvalidArgument,
                exception.Message));
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unexpected error while creating order.");

            throw new RpcException(new Status(
                StatusCode.Internal,
                "An unexpected error occurred while creating the order."));
        }
    }

    private static void ValidateCreateOrderRequest(CreateOrderRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.CustomerId))
        {
            throw new RpcException(new Status(
                StatusCode.InvalidArgument,
                "customer_id is required."));
        }

        if (request.Items.Count == 0)
        {
            throw new RpcException(new Status(
                StatusCode.InvalidArgument,
                "items must contain at least one item."));
        }

        foreach (CreateOrderItem? item in request.Items)
        {
            if (string.IsNullOrWhiteSpace(item.ProductId))
            {
                throw new RpcException(new Status(
                    StatusCode.InvalidArgument,
                    "items.product_id is required."));
            }

            if (string.IsNullOrWhiteSpace(item.ProductName))
            {
                throw new RpcException(new Status(
                    StatusCode.InvalidArgument,
                    "items.product_name is required."));
            }

            if (item.Quantity <= 0)
            {
                throw new RpcException(new Status(
                    StatusCode.InvalidArgument,
                    "items.quantity must be greater than zero."));
            }

            if (item.UnitPrice is null)
            {
                throw new RpcException(new Status(
                    StatusCode.InvalidArgument,
                    "items.unit_price is required."));
            }

            if (string.IsNullOrWhiteSpace(item.UnitPrice.CurrencyCode))
            {
                throw new RpcException(new Status(
                    StatusCode.InvalidArgument,
                    "items.unit_price.currency_code is required."));
            }

            if (item.UnitPrice.Units < 0 || item.UnitPrice.Nanos < 0)
            {
                throw new RpcException(new Status(
                    StatusCode.InvalidArgument,
                    "items.unit_price must not be negative."));
            }
        }
    }
}
