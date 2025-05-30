namespace SeatBooking.Domain.AircraftAggregate;

public class Aircraft
{
    private readonly List<Cabin> _cabins = new();
    protected Aircraft()
    {
    }

    public Aircraft(string code, string name)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Aircraft code cannot be null or empty.", nameof(code));

        Code = code;
        Name = name;

    }
    public  int Id { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; } 

    public IReadOnlyCollection<Cabin> Cabins => _cabins.AsReadOnly();

    public void AddCabin(Cabin cabin)
    {
        if (cabin == null)
            throw new ArgumentNullException(nameof(cabin));

        _cabins.Add(cabin);
    }

    public void RemoveCabin(Cabin cabin)
    {
        if (cabin == null)
            throw new ArgumentNullException(nameof(cabin));

        _cabins.Remove(cabin);
    }
}