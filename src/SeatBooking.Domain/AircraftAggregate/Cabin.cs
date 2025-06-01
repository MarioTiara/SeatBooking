namespace SeatBooking.Domain.AircraftAggregate;

/// <summary>
/// Cabin is an aggregate root for seat rows and columns within an aircraft.
/// </summary>
public class Cabin
{
    private readonly List<SeatRow> _seatRows = new();
    private readonly List<SeatColumn> _seatColumns = new();

    public int Id { get; private set; }
    public string Deck { get; private set; }
    public string AircraftCode { get; private set; }

    // Navigation property to parent aggregate
    public Aircraft Aircraft { get; private set; }

    // Expose collections as read-only for encapsulation
    public IReadOnlyCollection<SeatRow> SeatRows => _seatRows.AsReadOnly();
    public IReadOnlyCollection<SeatColumn> SeatColumns => _seatColumns.AsReadOnly();

    protected Cabin() { } // For EF Core

    public Cabin(string deck, string aircraftCode)
    {

        Deck = deck;
        AircraftCode = aircraftCode;
    }

    public void AddSeatRow(SeatRow seatRow)
    {
        if (seatRow == null)
            throw new ArgumentNullException(nameof(seatRow));
        _seatRows.Add(seatRow);
    }

    public void RemoveSeatRow(SeatRow seatRow)
    {
        if (seatRow == null)
            throw new ArgumentNullException(nameof(seatRow));
        _seatRows.Remove(seatRow);
    }

    public void AddSeatColumn(SeatColumn seatColumn)
    {
        if (seatColumn == null)
            throw new ArgumentNullException(nameof(seatColumn));
        _seatColumns.Add(seatColumn);
    }

    public void RemoveSeatColumn(SeatColumn seatColumn)
    {
        if (seatColumn == null)
            throw new ArgumentNullException(nameof(seatColumn));
        _seatColumns.Remove(seatColumn);
    }
}