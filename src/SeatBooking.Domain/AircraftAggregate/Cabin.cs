namespace SeatBooking.Domain.AircraftAggregate;

public class Cabin
{
    private readonly List<SeatRow> _seatRows = new();

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
}