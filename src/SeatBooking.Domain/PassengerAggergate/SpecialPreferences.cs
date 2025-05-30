namespace SeatBooking.Domain.PassengerAggregate;

public class SpecialPreferences
{
    public SpecialPreferences(
        string mealPreference,
        string seatPreference,
        List<string> specialRequests,
        List<string> specialServiceRequestRemarks)
    {
        MealPreference = mealPreference ?? throw new ArgumentNullException(nameof(mealPreference));
        SeatPreference = seatPreference ?? throw new ArgumentNullException(nameof(seatPreference));
        SpecialRequests = specialRequests ?? throw new ArgumentNullException(nameof(specialRequests));
        SpecialServiceRequestRemarks = specialServiceRequestRemarks ?? throw new ArgumentNullException(nameof(specialServiceRequestRemarks));
    }

    protected SpecialPreferences()
    {
        SpecialRequests = new List<string>();
        SpecialServiceRequestRemarks = new List<string>();
    }

    public string MealPreference { get; }
    public string SeatPreference { get; }
    public IReadOnlyList<string> SpecialRequests { get; }
    public IReadOnlyList<string> SpecialServiceRequestRemarks { get; }

    public override bool Equals(object? obj)
    {
        if (obj is not SpecialPreferences other) return false;

        return MealPreference == other.MealPreference &&
               SeatPreference == other.SeatPreference &&
               SpecialRequests.SequenceEqual(other.SpecialRequests) &&
               SpecialServiceRequestRemarks.SequenceEqual(other.SpecialServiceRequestRemarks);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(
            MealPreference,
            SeatPreference,
            string.Join(",", SpecialRequests),
            string.Join(",", SpecialServiceRequestRemarks));
    }
}
