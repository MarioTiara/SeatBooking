namespace SeatBooking.Domain.Shared;

public class Airport
{
    protected Airport()
    {
    }
    public Airport(string code, string name)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Code cannot be null or empty.", nameof(code));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));

        Code = code;
        Name = name;
    }

    
    public string Code { get; private set; }
    
    public string Name { get; private set; }
}