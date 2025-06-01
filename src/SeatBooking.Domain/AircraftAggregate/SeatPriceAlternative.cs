namespace SeatBooking.Domain.AircraftAggregate;

public class SeatPriceAlternative
{
    private readonly List<SeatPriceComponent> _components = new();
    
    // For EF Core
    protected SeatPriceAlternative() { }

    public SeatPriceAlternative(int seatSlotId)
    {
            
        SeatSlotId = seatSlotId;
    }

    public int Id { get; private set; }
    public int SeatSlotId { get; private set; }
    public SeatSlot SeatSlot { get; private set; } = default!;
    public IReadOnlyCollection<SeatPriceComponent> Components => _components.AsReadOnly();

    public void AddComponent(SeatPriceComponent component)
    {
        if (component == null) 
            throw new ArgumentNullException(nameof(component));
        component.SeatPriceAlternative= this; // Ensure the relationship is set
        _components.Add(component);
    }
}