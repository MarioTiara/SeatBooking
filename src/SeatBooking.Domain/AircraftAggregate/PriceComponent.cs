namespace SeatBooking.Domain.AircraftAggregate;

// Value Object for DDD
public class SeatPriceComponent
{
    public int Id { get; private set; }
    public decimal Amount { get; }
    public string Currency { get; }
    public int SeatPriceAlternativeId { get; private set; }
    public SeatPriceAlternative SeatPriceAlternative { get; set; }

    // For EF Core
    private SeatPriceComponent() { }

    public SeatPriceComponent(decimal amount, string currency)
    {

        Amount = amount;
        Currency = currency;
    }
  

    // Equality members for value object semantics
    public override bool Equals(object? obj)
    {
        if (obj is not SeatPriceComponent other) return false;
        return Amount == other.Amount && Currency == other.Currency;
    }

    public override int GetHashCode() => HashCode.Combine(Amount, Currency);
}