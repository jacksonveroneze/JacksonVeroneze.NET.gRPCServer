namespace JacksonVeroneze.NET.GRPCServer.Application.Orders.ListOrders;

public sealed class ListOrdersUseCase(
    IOrderRepository orderRepository)
{
    private const int DefaultPageSize = 20;
    private const int MaxPageSize = 100;

    public async Task<IReadOnlyCollection<Domain.Orders.Order>> ExecuteAsync(
        ListOrdersQuery query,
        CancellationToken cancellationToken)
    {
        int pageSize = NormalizePageSize(query.PageSize);

        return await orderRepository.ListAsync(
            query.CustomerId,
            query.Status,
            pageSize,
            query.PageToken,
            cancellationToken);
    }

    private static int NormalizePageSize(int pageSize)
    {
        if (pageSize <= 0)
        {
            return DefaultPageSize;
        }

        return Math.Min(pageSize, MaxPageSize);
    }
}
