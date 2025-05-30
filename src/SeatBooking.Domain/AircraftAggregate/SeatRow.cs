namespace SeatBooking.Domain.AircraftAggregate;

public class SeatRow
{
    private readonly List<SeatSlot> _seatSlots = new();

    public SeatRow(int rowNumber)
    {
        if (rowNumber <= 0)
            throw new ArgumentException("Row number must be greater than zero.", nameof(rowNumber));

        RowNumber = rowNumber;
    }
protected SeatRow()
    {
    }

    public int Id { get; private set; }

    public int RowNumber { get; private set; }

    public IReadOnlyCollection<SeatSlot> SeatSlots => _seatSlots.AsReadOnly();

    public int CabinId { get; private set; }
    public Cabin Cabin { get; private set; } = default!;

    public void AddSeatSlot(SeatSlot seatSlot)
    {
        if (seatSlot == null)
            throw new ArgumentNullException(nameof(seatSlot));

        _seatSlots.Add(seatSlot);
    }

    public void RemoveSeatSlot(SeatSlot seatSlot)
    {
        if (seatSlot == null)
            throw new ArgumentNullException(nameof(seatSlot));

        _seatSlots.Remove(seatSlot);
    }
}