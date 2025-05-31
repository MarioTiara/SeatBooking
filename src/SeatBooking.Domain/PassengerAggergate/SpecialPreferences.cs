namespace SeatBooking.Domain.PassengerAggregate;

public class SpecialPreferences
{
    public SpecialPreferences(
        string mealPreference,
        string seatPreference,
        List<SpecialPreferences> specialRequests,
        List<SpecialServiceRequestRemark> specialServiceRequestRemarks)
    {
        MealPreference = mealPreference ?? throw new ArgumentNullException(nameof(mealPreference));
        SeatPreference = seatPreference ?? throw new ArgumentNullException(nameof(seatPreference));
        SpecialRequests = specialRequests ?? throw new ArgumentNullException(nameof(specialRequests));
        SpecialServiceRequestRemarks = specialServiceRequestRemarks ?? throw new ArgumentNullException(nameof(specialServiceRequestRemarks));
    }

    protected SpecialPreferences()
    {
        SpecialRequests = new List<SpecialPreferences>();
        SpecialServiceRequestRemarks = new List<SpecialServiceRequestRemark>();
    }

    public string MealPreference { get; }
    public string SeatPreference { get; }
    public IReadOnlyList<SpecialPreferences> SpecialRequests { get; }
    public IReadOnlyList<SpecialServiceRequestRemark> SpecialServiceRequestRemarks { get; }

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

public class SpecialRequest
{
    public int Id { get; set; }
    public string Value { get; set; } = default!;
    public int PassengerId { get; set; }
}

public class SpecialServiceRequestRemark
{
    public int Id { get; set; }
    public string Value { get; set; } = default!;
    public int PassengerId { get; set; }
}
