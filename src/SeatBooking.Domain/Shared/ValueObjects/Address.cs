namespace SeatBooking.Domain.Shared.ValueObjects;

public class Address
{
    protected Address()
    {
    }
    public Address(
        string street1,
        string street2,
        string postcode,
        string state,
        string city,
        string country,
        string addressType)
    {
        if (string.IsNullOrWhiteSpace(street1))
            throw new ArgumentException("Street1 cannot be null or empty.", nameof(street1));
        if (string.IsNullOrWhiteSpace(postcode))
            throw new ArgumentException("Postcode cannot be null or empty.", nameof(postcode));
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City cannot be null or empty.", nameof(city));
        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("Country cannot be null or empty.", nameof(country));
        if (string.IsNullOrWhiteSpace(addressType))
            throw new ArgumentException("AddressType cannot be null or empty.", nameof(addressType));

        Street1 = street1;
        Street2 = street2;
        Postcode = postcode;
        State = state;
        City = city;
        Country = country;
        AddressType = addressType;
    }

    public string Street1 { get; }
    public string Street2 { get; }
    public string Postcode { get; }
    public string State { get; }
    public string City { get; }
    public string Country { get; }
    public string AddressType { get; }

    public override bool Equals(object? obj)
    {
        if (obj is not Address other) return false;

        return Street1 == other.Street1 &&
               Street2 == other.Street2 &&
               Postcode == other.Postcode &&
               State == other.State &&
               City == other.City &&
               Country == other.Country &&
               AddressType == other.AddressType;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Street1, Street2, Postcode, State, City, Country, AddressType);
    }
}