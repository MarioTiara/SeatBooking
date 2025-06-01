namespace SeatBooking.Domain.AircraftAggregate;

public class SeatTaxAlternative
{
    private readonly List<SeatTaxComponent> _components = new();

    protected SeatTaxAlternative() { }

    public SeatTaxAlternative(int seatSlotId)
    {
        SeatSlotId = seatSlotId;
    }

    public int Id { get; private set; }
    public int SeatSlotId { get; private set; }
    public SeatSlot SeatSlot { get; private set; } = default!;

    public IReadOnlyCollection<SeatTaxComponent> Components => _components.AsReadOnly();

    public void AddComponent(SeatTaxComponent component)
    {
        if (component == null) 
            throw new ArgumentNullException(nameof(component));
        component.SeatTaxAlternative = this; 
        _components.Add(component);
    }
}
