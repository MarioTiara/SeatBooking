namespace SeatBooking.Domain.AircraftAggregate;

public class SeatPriceComponent
{
    public SeatPriceComponent(decimal amount, string currency)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.", nameof(amount));

        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency cannot be null or empty.", nameof(currency));

        Amount = amount;
        Currency = currency;
    }
protected SeatPriceComponent()
    {
    }

    public int Id { get; private set; }

    public decimal Amount { get; private set; }

    public string Currency { get; private set; }

    public int SeatPriceAlternativeId { get; private set; }
    public SeatPriceAlternative SeatPriceAlternative { get; private set; } = default!;
}
