namespace SeatBooking.Domain.AircraftAggregate;


public class SeatTaxAlternative
{
    public int Id { get; private set; }
    public int SeatSlotId { get; private set; }
    public SeatSlot SeatSlot { get; private set; } = default!;

    private readonly List<SeatPriceComponent> _components = new();
    public IReadOnlyCollection<SeatPriceComponent> Components => _components.AsReadOnly();

    private SeatTaxAlternative() { }

    public SeatTaxAlternative(int seatSlotId)
    {
        SeatSlotId = seatSlotId;
    }

    public void AddComponent(SeatPriceComponent component)
    {
        if (component == null) throw new ArgumentNullException(nameof(component));
        _components.Add(component);
    }
}