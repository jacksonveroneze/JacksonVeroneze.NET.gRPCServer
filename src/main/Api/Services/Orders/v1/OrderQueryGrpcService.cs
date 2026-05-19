using Grpc.Core;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Orders.ListOrders;
using JacksonVeroneze.NET.GRPCServer.Contracts.Orders.v1;
using JacksonVeroneze.NET.Result;
using Microsoft.AspNetCore.Authorization;

namespace JacksonVeroneze.NET.GRPCServer.Api.Services.Orders.v1;

[Authorize(Policy = "OrdersList")]
public class OrderQueryGrpcService(
    IListOrdersUseCase useCase,
    ILogger<OrderQueryGrpcService> logger)
    : OrderQueryService.OrderQueryServiceBase
{
    private const int MaxPageSize = 100;

    public override async Task<ListOrdersResponse> ListOrders(
        ListOrdersRequest request,
        ServerCallContext context)
    {
        try
        {
            ValidateListOrdersRequest(request);

            ListOrdersInput input = OrderMapper.ToQuery(request);

            Result<ListOrdersOutput> result = await useCase.ExecuteAsync(
                input, context.CancellationToken);

            ListOrdersResponse response = new();

            return response;
        }
        catch (RpcException)
        {
            throw;
        }
        catch (OperationCanceledException) when (context.CancellationToken.IsCancellationRequested)
        {
            logger.LogWarning("ListOrders call was cancelled by the client.");

            throw new RpcException(new Status(
                StatusCode.Cancelled,
                "The operation was cancelled."));
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(exception, "Invalid ListOrders request.");

            throw new RpcException(new Status(
                StatusCode.InvalidArgument,
                exception.Message));
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unexpected error while listing orders.");

            throw new RpcException(new Status(
                StatusCode.Internal,
                "An unexpected error occurred while listing orders."));
        }
    }

    private static void ValidateListOrdersRequest(ListOrdersRequest request)
    {
        if (request.PageSize < 0)
        {
            throw new RpcException(new Status(
                StatusCode.InvalidArgument,
                "page_size must be greater than or equal to zero."));
        }

        if (request.PageSize > MaxPageSize)
        {
            throw new RpcException(new Status(
                StatusCode.InvalidArgument,
                $"page_size must be less than or equal to {MaxPageSize}."));
        }
    }
}
