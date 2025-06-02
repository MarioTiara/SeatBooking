using SeatBooking.Domain.PassengerAggregate;

namespace SeatBooking.Domain.AircraftAggregate;

// SeatSlot.cs
public class SeatSlot
{
    public int Id { get; private set; }
    public string StorefrontSlotCode { get; private set; }
    public string? Code { get; private set; }
    public bool Available { get; private set; }
    public bool Entitled { get; private set; }
    public bool FreeOfCharge { get; private set; }
    public bool FeeWaived { get; private set; }
    public bool OriginallySelected { get; private set; }

    public string? EntitledRuleId { get; private set; }
    public string? FeeWaivedRuleId { get; private set; }
    public string? RefundIndicator { get; private set; }

    private readonly List<string> _seatCharacteristics = new();
    public IReadOnlyCollection<string> SeatCharacteristics => _seatCharacteristics.AsReadOnly();
    private readonly List<string> _slotCharacteristics = new();
    public IReadOnlyCollection<string> SlotCharacteristics => _slotCharacteristics.AsReadOnly();

    private readonly List<string> _rawSeatCharacteristics = new();
    public IReadOnlyCollection<string> RawSeatCharacteristics => _rawSeatCharacteristics.AsReadOnly();


    private readonly List<SeatPriceAlternative> _priceAlternatives = new();
    public IReadOnlyCollection<SeatPriceAlternative> PriceAlternatives => _priceAlternatives.AsReadOnly();

    private readonly List<SeatTaxAlternative> _taxes = new();
    public IReadOnlyCollection<SeatTaxAlternative> Taxes => _taxes.AsReadOnly();

    private readonly List<string> _designations = new();
    public IReadOnlyCollection<string> Designations => _designations.AsReadOnly();

    private readonly List<string> _limitations = new();
    public IReadOnlyCollection<string> Limitations => _limitations.AsReadOnly();

    private readonly List<PassengerSeatSelection> _seatSelections = new();
    public IReadOnlyCollection<PassengerSeatSelection> SeatSelections => _seatSelections.AsReadOnly();

    public int SeatRowId { get; private set; }
    public SeatRow SeatRow { get; private set; } = default!;

    public SeatSlot(
        string storefrontSlotCode,
        string? code,
        bool available,
        bool entitled,
        bool freeOfCharge,
        bool feeWaived,
        bool originallySelected,
        string? entitledRuleId,
        string? feeWaivedRuleId,
        string? refundIndicator)
    {

        StorefrontSlotCode = storefrontSlotCode;
        Code = code;
        Available = available;
        Entitled = entitled;
        FreeOfCharge = freeOfCharge;
        FeeWaived = feeWaived;
        OriginallySelected = originallySelected;
        EntitledRuleId = entitledRuleId;
        FeeWaivedRuleId = feeWaivedRuleId;
        RefundIndicator = refundIndicator;
    }

    protected SeatSlot() { }

    public void MarkSelected()
    {
        if (Available)
        {
            Available = false;
            OriginallySelected = true;
        }
    }

    public void AddPriceAlternative(SeatPriceAlternative alt) => _priceAlternatives.Add(alt);
    public void AddTax(SeatTaxAlternative tax) => _taxes.Add(tax);
    public void AddDesignation(string d) => _designations.Add(d);
    public void AddRawSeatCharacteristic(string characteristic)
    {
        if (characteristic == null) throw new ArgumentNullException(nameof(characteristic));
        _rawSeatCharacteristics.Add(characteristic);
    }
    public void AddSeatCharacteristic(string ch)
    {
        if (ch == null) throw new ArgumentNullException(nameof(ch));
        _seatCharacteristics.Add(ch);
    }
    public void AddSlotCharacteristic(string ch)
    {
        if (ch == null) throw new ArgumentNullException(nameof(ch));
        _slotCharacteristics.Add(ch);
    }
    public void AddLimitation(string l) => _limitations.Add(l);
    public void AddSeatSelection(PassengerSeatSelection selection) => _seatSelections.Add(selection);


   
}