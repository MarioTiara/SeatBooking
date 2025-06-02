using SeatBooking.Domain.Shared.ValueObjects;
using SeatBooking.Domain.Shared;
using SeatBooking.Domain.AircraftAggregate;

namespace SeatBooking.Domain.PassengerAggregate;

public class Passenger
{
    private readonly List<Email> _emails = new();
    private readonly List<Phone> _phones = new();
    private readonly List<FrequentFlyer> _frequentFlyers = new();

    public Passenger(
        int passengerIndex,
        string passengerNameNumber,
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        string gender,
        string type,
        Address address,
        SpecialPreferences specialPreferences,
        DocumentInfo documentInfo)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be null or empty.", nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be null or empty.", nameof(lastName));
        if (string.IsNullOrWhiteSpace(gender))
            throw new ArgumentException("Gender cannot be null or empty.", nameof(gender));
        if (string.IsNullOrWhiteSpace(type))
            throw new ArgumentException("Type cannot be null or empty.", nameof(type));
        if (address == null)
            throw new ArgumentNullException(nameof(address));
        if (specialPreferences == null)
            throw new ArgumentNullException(nameof(specialPreferences));
        if (documentInfo == null)
            throw new ArgumentNullException(nameof(documentInfo));
        if (string.IsNullOrWhiteSpace(passengerNameNumber))
            throw new ArgumentException("Passenger name number cannot be null or empty.", nameof(passengerNameNumber));
        if (passengerIndex < 0)
            throw new ArgumentOutOfRangeException(nameof(passengerIndex), "Passenger index cannot be negative.");

        PassengerIndex = passengerIndex;
        PassengerNameNumber = passengerNameNumber;

        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        Type = type;
        Address = address;
        SpecialPreferences = specialPreferences;
        DocumentInfo = documentInfo;
    }

    protected Passenger()
    {

    }

    public int Id { get; private set; }
    public int PassengerIndex { get; private set; }
    public string PassengerNameNumber { get; private set; } = default!;

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public string Gender { get; private set; }
    public string Type { get; private set; }

    public IReadOnlyCollection<Email> Emails => _emails.AsReadOnly();
    public IReadOnlyCollection<Phone> Phones => _phones.AsReadOnly();
    public Address Address { get; private set; }
    public SpecialPreferences SpecialPreferences { get; private set; }
    public IReadOnlyCollection<FrequentFlyer> FrequentFlyers => _frequentFlyers.AsReadOnly();
    public DocumentInfo DocumentInfo { get; private set; }
    public PassengerSeatSelection? SeatSelection { get; private set; }

    public void SetSeatSelection(SeatSlot slot)
    {
        if (slot == null)
            throw new ArgumentNullException(nameof(slot));
        SeatSelection = new PassengerSeatSelection(this,slot);
    }

    public void AddEmail(Email email)
    {
        if (email == null)
            throw new ArgumentNullException(nameof(email));

        _emails.Add(email);
    }

    public void RemoveEmail(Email email)
    {
        if (email == null)
            throw new ArgumentNullException(nameof(email));

        _emails.Remove(email);
    }

    public void AddPhone(Phone phone)
    {
        if (phone == null)
            throw new ArgumentNullException(nameof(phone));

        _phones.Add(phone);
    }

    public void RemovePhone(Phone phone)
    {
        if (phone == null)
            throw new ArgumentNullException(nameof(phone));

        _phones.Remove(phone);
    }

    public void AddFrequentFlyer(FrequentFlyer frequentFlyer)
    {
        if (frequentFlyer == null)
            throw new ArgumentNullException(nameof(frequentFlyer));

        _frequentFlyers.Add(frequentFlyer);
    }

    public void RemoveFrequentFlyer(FrequentFlyer frequentFlyer)
    {
        if (frequentFlyer == null)
            throw new ArgumentNullException(nameof(frequentFlyer));

        _frequentFlyers.Remove(frequentFlyer);
    }

    public void UpdateSeatSelection(PassengerSeatSelection seatSelection)
    {
        SeatSelection = seatSelection ?? throw new ArgumentNullException(nameof(seatSelection));
    }
}