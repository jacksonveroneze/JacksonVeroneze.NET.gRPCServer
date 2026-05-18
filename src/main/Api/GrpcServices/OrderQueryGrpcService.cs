using Grpc.Core;
using JacksonVeroneze.NET.GRPCServer.Api.Application.Orders.ListOrders;
using JacksonVeroneze.NET.GRPCServer.Api.Mapping;

namespace JacksonVeroneze.NET.GRPCServer.Api.GrpcServices;

public class OrderQueryGrpcService(
    ListOrdersUseCase useCase,
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

            ListOrdersQuery query = OrderGrpcMapper.ToQuery(request);

            IReadOnlyCollection<Domain.Orders.Order> orders = await useCase.ExecuteAsync(
                query,
                context.CancellationToken);

            ListOrdersResponse response = new();

            response.Orders.AddRange(
                orders.Select(OrderGrpcMapper.ToGrpcOrderSummary));

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