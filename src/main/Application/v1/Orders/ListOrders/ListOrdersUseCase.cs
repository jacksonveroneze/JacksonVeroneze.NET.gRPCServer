using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.Repositories;
using JacksonVeroneze.NET.Result;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Orders.ListOrders;

public sealed class ListOrdersUseCase(
    IOrderReadRepository orderRepository) : IListOrdersUseCase
{
    private const int DefaultPageSize = 20;
    private const int MaxPageSize = 100;

    public async Task<Result<ListOrdersOutput>> ExecuteAsync(
        ListOrdersInput input,
        CancellationToken cancellationToken)
    {
        int pageSize = NormalizePageSize(input.PageSize);

        _ = await orderRepository.ListAsync(
            input.CustomerId,
            input.Status,
            pageSize,
            input.PageToken,
            cancellationToken);

        ListOrdersOutput result = new();

        return Result<ListOrdersOutput>.WithSuccess(result);
    }

    private static int NormalizePageSize(int pageSize)
    {
        return pageSize <= 0 
            ? DefaultPageSize 
            : Math.Min(pageSize, MaxPageSize);
    }
}
