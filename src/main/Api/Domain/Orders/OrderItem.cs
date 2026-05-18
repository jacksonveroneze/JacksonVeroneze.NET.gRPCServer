namespace JacksonVeroneze.NET.GRPCServer.Api.Domain.Orders;

public sealed class OrderItem
{
    public OrderItem(
        string productId,
        string productName,
        int quantity,
        Money unitPrice)
    {
        if (string.IsNullOrWhiteSpace(productId))
        {
            throw new ArgumentException("Product id is required.", nameof(productId));
        }

        if (string.IsNullOrWhiteSpace(productName))
        {
            throw new ArgumentException("Product name is required.", nameof(productName));
        }

        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
        }

        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        TotalPrice = unitPrice.Multiply(quantity);
    }

    public string ProductId { get; }
    public string ProductName { get; }
    public int Quantity { get; }
    public Money UnitPrice { get; }
    public Money TotalPrice { get; }
}