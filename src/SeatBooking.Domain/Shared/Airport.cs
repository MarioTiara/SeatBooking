namespace SeatBooking.Domain.Shared;

public class Airport
{
    protected Airport()
    {
    }
    public Airport(string code, string name, string city)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Code cannot be null or empty.", nameof(code));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City cannot be null or empty.", nameof(city));

        Code = code;
        Name = name;
        City    = city;

    }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string City { get; private set; } 
}