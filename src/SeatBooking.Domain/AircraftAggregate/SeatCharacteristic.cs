namespace SeatBooking.Domain.AircraftAggregate;

public class SeatCharacteristic
{
    public SeatCharacteristic(string code, bool isRaw, bool isslot)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Code cannot be null or empty.", nameof(code));

        Code = code;
        IsRaw = isRaw;
        IsSlot = isslot;
    }
    protected SeatCharacteristic    ()
    {
    }

    public int Id { get; private set; }

    public string Code { get; private set; }

    public bool IsRaw { get; private set; }
    public bool IsSlot { get; private set; }

    public int SeatSlotId { get; private set; }
    public SeatSlot SeatSlot { get; private set; } = default!;
}