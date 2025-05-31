namespace SeatBooking.Domain.AircraftAggregate;

public class Cabin
{
    public int Id { get; private set; }
    public string Deck { get; private set; }
    public string AircraftCode { get; private set; } = default!;

    // Navigation properties
    public Aircraft Aircraft { get; private set; } = default!;
    public ICollection<SeatRow> SeatRows { get; private set; } = new List<SeatRow>();
    public ICollection<SeatColumn> SeatColumns { get; private set; } = new List<SeatColumn>();

    // Behavior: Add a seat row to the cabin
    public void AddSeatRow(SeatRow seatRow)
    {
        if (seatRow == null)
            throw new ArgumentNullException(nameof(seatRow));

        SeatRows.Add(seatRow);
    }

    // Behavior: Remove a seat row from the cabin
    public void RemoveSeatRow(SeatRow seatRow)
    {
        if (seatRow == null)
            throw new ArgumentNullException(nameof(seatRow));

        SeatRows.Remove(seatRow);
    }

    // Behavior: Add a seat column to the cabin
    public void AddSeatColumn(SeatColumn seatColumn)
    {
        if (seatColumn == null)
            throw new ArgumentNullException(nameof(seatColumn));

        SeatColumns.Add(seatColumn);
    }

    // Behavior: Remove a seat column from the cabin
    public void RemoveSeatColumn(SeatColumn seatColumn)
    {
        if (seatColumn == null)
            throw new ArgumentNullException(nameof(seatColumn));

        SeatColumns.Remove(seatColumn);
    }
}