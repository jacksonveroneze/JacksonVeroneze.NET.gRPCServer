namespace JacksonVeroneze.NET.GRPCServer.Api.Domain.Orders;

public sealed class Order
{
    private readonly List<OrderItem> _items;

    private Order(
        string orderId,
        string customerId,
        IReadOnlyCollection<OrderItem> items,
        DateTimeOffset createdAt)
    {
        OrderId = orderId;
        CustomerId = customerId;
        _items = items.ToList();
        Status = OrderStatus.Pending;
        CreatedAt = createdAt;
        TotalAmount = CalculateTotal(_items);
    }

    public string OrderId { get; }
    public string CustomerId { get; }
    public IReadOnlyCollection<OrderItem> Items => _items;
    public OrderStatus Status { get; private set; }
    public Money TotalAmount { get; }
    public DateTimeOffset CreatedAt { get; }

    public static Order Create(
        string customerId,
        IReadOnlyCollection<OrderItem> items)
    {
        if (string.IsNullOrWhiteSpace(customerId))
        {
            throw new ArgumentException("Customer id is required.", nameof(customerId));
        }

        if (items.Count == 0)
        {
            throw new ArgumentException("Order must contain at least one item.", nameof(items));
        }

        return new Order(
            orderId: Guid.NewGuid().ToString("N"),
            customerId: customerId,
            items: items,
            createdAt: DateTimeOffset.UtcNow);
    }

    private static Money CalculateTotal(IEnumerable<OrderItem> items)
    {
        List<OrderItem> itemList = items.ToList();

        if (itemList.Count == 0)
        {
            return Money.Zero();
        }

        Money total = Money.Zero(itemList[0].UnitPrice.CurrencyCode);

        foreach (OrderItem item in itemList)
        {
            total = Money.Add(total, item.TotalPrice);
        }

        return total;
    }
}