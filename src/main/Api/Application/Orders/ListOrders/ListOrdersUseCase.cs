namespace JacksonVeroneze.NET.GRPCServer.Api.Application.Orders.ListOrders;

public sealed class ListOrdersUseCase
{
    private const int DefaultPageSize = 20;
    private const int MaxPageSize = 100;

    private readonly IOrderRepository _orderRepository;

    public ListOrdersUseCase(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IReadOnlyCollection<Domain.Orders.Order>> ExecuteAsync(
        ListOrdersQuery query,
        CancellationToken cancellationToken)
    {
        int pageSize = NormalizePageSize(query.PageSize);

        return await _orderRepository.ListAsync(
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