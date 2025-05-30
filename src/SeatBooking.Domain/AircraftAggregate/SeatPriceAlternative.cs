namespace SeatBooking.Domain.AircraftAggregate;

public class SeatPriceAlternative
{
    private readonly List<SeatPriceComponent> _components = new();

    public SeatPriceAlternative(int seatSlotId)
    {
        if (seatSlotId <= 0)
            throw new ArgumentException("SeatSlotId must be greater than zero.", nameof(seatSlotId));

        SeatSlotId = seatSlotId;
    }

    protected SeatPriceAlternative()
    {
          
    }

    public int Id { get; private set; }

    public IReadOnlyCollection<SeatPriceComponent> Components => _components.AsReadOnly();

    public int SeatSlotId { get; private set; }
    public SeatSlot SeatSlot { get; private set; } = default!;

    public void AddComponent(SeatPriceComponent component)
    {
        if (component == null)
            throw new ArgumentNullException(nameof(component));

        _components.Add(component);
    }

    public void RemoveComponent(SeatPriceComponent component)
    {
        if (component == null)
            throw new ArgumentNullException(nameof(component));

        _components.Remove(component);
    }
}
