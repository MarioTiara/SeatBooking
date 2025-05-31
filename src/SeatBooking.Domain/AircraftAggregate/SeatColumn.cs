namespace SeatBooking.Domain.AircraftAggregate;

public class SeatColumn
{
    protected SeatColumn() { }

    public SeatColumn(string code, int cabinId)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Code cannot be null or empty.", nameof(code));
        if (cabinId <= 0)
            throw new ArgumentException("CabinId must be greater than zero.", nameof(cabinId));

        Code = code;
        CabinId = cabinId;
    }

    public int Id { get; private set; }
    public string Code { get; private set; }
    public int CabinId { get; private set; }
    public Cabin Cabin { get; private set; } = default!;
}