namespace SeatBooking.Domain.AircraftAggregate;

// Value Object for DDD
public class SlotDesignation
{
    public string Code { get; }

    // For EF Core
    protected SlotDesignation() { }

    public SlotDesignation(string code)
    {
        Code = code;
    }

    // Equality members for value object semantics
    public override bool Equals(object? obj)
    {
        if (obj is not SlotDesignation other) return false;
        return Code == other.Code;
    }

    public override int GetHashCode() => Code.GetHashCode();
}