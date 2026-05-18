namespace JacksonVeroneze.NET.GRPCServer.Api.Domain.Orders;

public sealed record Money(
    string CurrencyCode,
    long Units,
    int Nanos)
{
    public static Money Zero(string currencyCode = "BRL")
    {
        return new Money(currencyCode, 0, 0);
    }

    public Money Multiply(int quantity)
    {
        return new Money(
            CurrencyCode,
            Units * quantity,
            Nanos * quantity);
    }

    public static Money Add(Money left, Money right)
    {
        if (!string.Equals(left.CurrencyCode, right.CurrencyCode, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Cannot add money with different currencies.");
        }

        int nanos = left.Nanos + right.Nanos;
        long units = left.Units + right.Units;

        if (nanos >= 1_000_000_000)
        {
            units += nanos / 1_000_000_000;
            nanos %= 1_000_000_000;
        }

        return new Money(left.CurrencyCode, units, nanos);
    }
}