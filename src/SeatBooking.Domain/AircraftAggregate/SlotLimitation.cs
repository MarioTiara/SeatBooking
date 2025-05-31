namespace SeatBooking.Domain.AircraftAggregate;

// Value Object for DDD
public class SlotLimitation
{
    public string Code { get; }

    // For EF Core
    private SlotLimitation() { }

    public SlotLimitation(string code)
    {
        Code = code;
    }

    // Equality members for value object semantics
    public override bool Equals(object? obj)
    {
        if (obj is not SlotLimitation other) return false;
        return Code == other.Code;
    }

    public override int GetHashCode() => Code.GetHashCode();
}