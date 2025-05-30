namespace SeatBooking.Domain.Shared.ValueObjects;

public class Phone
{
    protected Phone()
    {
    }
    public Phone(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number cannot be null or empty.", nameof(phoneNumber));
        if (!IsValidPhoneNumber(phoneNumber))
            throw new ArgumentException("Invalid phone number format.", nameof(phoneNumber));

        Value = phoneNumber;
    }

    public string Value { get; }

    private static bool IsValidPhoneNumber(string phoneNumber)
    {
        // Basic validation for phone number format (can be extended as needed)
        return phoneNumber.All(char.IsDigit) && phoneNumber.Length >= 7 && phoneNumber.Length <= 15;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Phone other) return false;

        return Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}