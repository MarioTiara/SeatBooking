namespace SeatBooking.Domain.AircraftAggregate;

public class SeatCharacteristic
{
    public SeatCharacteristic(string code, bool isRaw)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Code cannot be null or empty.", nameof(code));

        Code = code;
        IsRaw = isRaw;
    }
    protected SeatCharacteristic    ()
    {
    }

    public int Id { get; private set; }

    public string Code { get; private set; }

    public bool IsRaw { get; private set; }

    public int SeatSlotId { get; private set; }
    public SeatSlot SeatSlot { get; private set; } = default!;
}