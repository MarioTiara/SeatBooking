namespace SeatBooking.Domain.AircraftAggregate;

public class Cabin
{
    private readonly List<SeatRow> _seatRows = new();
    private readonly List<SeatColumn> _seatColumns = new();

    protected Cabin()
    {
    }
    public Cabin(string deck)
    {
        if (string.IsNullOrWhiteSpace(deck))
            throw new ArgumentException("Deck cannot be null or empty.", nameof(deck));

        Deck = deck;
    }

    public int Id { get; private set; }

    public string Deck { get; private set; }

    public IReadOnlyCollection<SeatRow> SeatRows => _seatRows.AsReadOnly();
    public IReadOnlyCollection<SeatColumn> SeatColumns => _seatColumns.AsReadOnly();

    // Behavior: Add a seat row to the cabin
    public void AddSeatRow(SeatRow seatRow)
    {
        if (seatRow == null)
            throw new ArgumentNullException(nameof(seatRow));

        _seatRows.Add(seatRow);
    }

    // Behavior: Remove a seat row from the cabin
    public void RemoveSeatRow(SeatRow seatRow)
    {
        if (seatRow == null)
            throw new ArgumentNullException(nameof(seatRow));

        _seatRows.Remove(seatRow);
    }

    // Behavior: Add a seat column to the cabin
    public void AddSeatColumn(SeatColumn seatColumn)
    {
        if (seatColumn == null)
            throw new ArgumentNullException(nameof(seatColumn));

        _seatColumns.Add(seatColumn);
    }

    // Behavior: Remove a seat column from the cabin
    public void RemoveSeatColumn(SeatColumn seatColumn)
    {
        if (seatColumn == null)
            throw new ArgumentNullException(nameof(seatColumn));

        _seatColumns.Remove(seatColumn);
    }
}