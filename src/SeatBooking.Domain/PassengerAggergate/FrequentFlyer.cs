namespace SeatBooking.Domain.PassengerAggregate;

public class FrequentFlyer
{
    public FrequentFlyer(string airline, string number, int tierNumber)
    {
        if (string.IsNullOrWhiteSpace(airline))
            throw new ArgumentException("Airline cannot be null or empty.", nameof(airline));
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Number cannot be null or empty.", nameof(number));
        if (tierNumber < 0)
            throw new ArgumentException("Tier number cannot be negative.", nameof(tierNumber));

        Airline = airline;
        Number = number;
        TierNumber = tierNumber;
    }
    protected FrequentFlyer() 
    {
    }

    public int Id { get; private set; }
    public string Airline { get; private set; }
    public string Number { get; private set; }
    public int TierNumber { get; private set; }

    public void UpdateTier(int newTierNumber)
    {
        if (newTierNumber < 0)
            throw new ArgumentException("Tier number cannot be negative.", nameof(newTierNumber));

        TierNumber = newTierNumber;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not FrequentFlyer other) return false;

        return Airline == other.Airline &&
               Number == other.Number &&
               TierNumber == other.TierNumber;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Airline, Number, TierNumber);
    }
}