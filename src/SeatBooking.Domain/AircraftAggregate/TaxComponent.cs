namespace SeatBooking.Domain.AircraftAggregate;

// Value Object for DDD
public class SeatTaxComponent
{
    public int Id { get; private set; }
    public decimal Amount { get; }
    public string Currency { get; }
    public int SeatTaxAlternativeId { get; private set; }
    public SeatTaxAlternative SeatTaxAlternative { get;  set; } = default!;

    // For EF Core
    private SeatTaxComponent() { }

    public SeatTaxComponent(decimal amount, string currency)
    {
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency cannot be null or empty.", nameof(currency));
        
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