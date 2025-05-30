using SeatBooking.Domain.PassengerAggregate;

namespace SeatBooking.Domain.AircraftAggregate;

public class SeatSlot
{
    private readonly List<SeatCharacteristic> _seatCharacteristics = new();
    private readonly List<SeatPriceAlternative> _priceAlternatives = new();
    private readonly List<PassengerSeatSelection> _seatSelections = new();

    public SeatSlot(string storefrontSlotCode, string? code, bool available, bool entitled, bool freeOfCharge)
    {
        if (string.IsNullOrWhiteSpace(storefrontSlotCode))
            throw new ArgumentException("StorefrontSlotCode cannot be null or empty.", nameof(storefrontSlotCode));

        StorefrontSlotCode = storefrontSlotCode;
        Code = code;
        Available = available;
        Entitled = entitled;
        FreeOfCharge = freeOfCharge;
    }
    protected SeatSlot()
    {
    }

    public int Id { get; private set; }
    public string StorefrontSlotCode { get; private set; }
    public string? Code { get; private set; }
    public bool Available { get; private set; }
    public bool Entitled { get; private set; }
    public bool FreeOfCharge { get; private set; }

    public IReadOnlyCollection<SeatCharacteristic> SeatCharacteristics => _seatCharacteristics.AsReadOnly();
    public IReadOnlyCollection<SeatPriceAlternative> PriceAlternatives => _priceAlternatives.AsReadOnly();
    public IReadOnlyCollection<PassengerSeatSelection> SeatSelections => _seatSelections.AsReadOnly();

    public int SeatRowId { get; private set; }
    public SeatRow SeatRow { get; private set; } = default!;

    public void AddSeatCharacteristic(SeatCharacteristic characteristic)
    {
        if (characteristic == null)
            throw new ArgumentNullException(nameof(characteristic));

        _seatCharacteristics.Add(characteristic);
    }

    public void RemoveSeatCharacteristic(SeatCharacteristic characteristic)
    {
        if (characteristic == null)
            throw new ArgumentNullException(nameof(characteristic));

        _seatCharacteristics.Remove(characteristic);
    }

    public void AddPriceAlternative(SeatPriceAlternative priceAlternative)
    {
        if (priceAlternative == null)
            throw new ArgumentNullException(nameof(priceAlternative));

        _priceAlternatives.Add(priceAlternative);
    }

    public void RemovePriceAlternative(SeatPriceAlternative priceAlternative)
    {
        if (priceAlternative == null)
            throw new ArgumentNullException(nameof(priceAlternative));

        _priceAlternatives.Remove(priceAlternative);
    }

    public void AddSeatSelection(PassengerSeatSelection seatSelection)
    {
        if (seatSelection == null)
            throw new ArgumentNullException(nameof(seatSelection));

        _seatSelections.Add(seatSelection);
    }

    public void RemoveSeatSelection(PassengerSeatSelection seatSelection)
    {
        if (seatSelection == null)
            throw new ArgumentNullException(nameof(seatSelection));

        _seatSelections.Remove(seatSelection);
    }
}