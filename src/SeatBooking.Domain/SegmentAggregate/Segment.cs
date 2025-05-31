using SeatBooking.Domain.SegmentAggregate;
using SeatBooking.Domain.Shared;

namespace SeatBooking.Domain.SegmentAggregate;

public class Segment
{
    protected Segment() // For EF Core
    {
    }
    public Segment(
        int flightsMiles,
        bool awardFare,
        int duration,
        string cabinClass,
        string equipment,
        FlightInfo flight,
        string originAirportCode,
        string destinationAirportCode,
        DateTime departure,
        DateTime arrival,
        string bookingClass,
        int layoverDuration,
        string fareBasis,
        bool subjectToGovernmentApproval,
        string segmentRef)
    {
        if (string.IsNullOrWhiteSpace(cabinClass))
            throw new ArgumentException("Cabin class cannot be null or empty.", nameof(cabinClass));
        if (string.IsNullOrWhiteSpace(equipment))
            throw new ArgumentException("Equipment cannot be null or empty.", nameof(equipment));
        if (flight == null)
            throw new ArgumentNullException(nameof(flight));
        if (string.IsNullOrWhiteSpace(bookingClass))
            throw new ArgumentException("Booking class cannot be null or empty.", nameof(bookingClass));
        if (string.IsNullOrWhiteSpace(fareBasis))
            throw new ArgumentException("Fare basis cannot be null or empty.", nameof(fareBasis));
        if (string.IsNullOrWhiteSpace(segmentRef))
            throw new ArgumentException("Segment reference cannot be null or empty.", nameof(segmentRef));

        FlightsMiles = flightsMiles;
        AwardFare = awardFare;
        Duration = duration;
        CabinClass = cabinClass;
        Equipment = equipment;
        Flight = flight;
        OriginAirportCode = originAirportCode;
        DestinationAirportCode = destinationAirportCode;
        Departure = departure;
        Arrival = arrival;
        BookingClass = bookingClass;
        LayoverDuration = layoverDuration;
        FareBasis = fareBasis;
        SubjectToGovernmentApproval = subjectToGovernmentApproval;
        SegmentRef = segmentRef;
    }

    public int Id { get; private set; }
    public int FlightsMiles { get; private set; }
    public bool AwardFare { get; private set; }
    public int Duration { get; private set; }
    public string CabinClass { get; private set; }
    public string Equipment { get; private set; }
    public FlightInfo Flight { get; private set; }
    public string OriginAirportCode { get; private set; }
    public string DestinationAirportCode { get; private set; }
    public Airport Origin { get; private set; }
    public Airport Destination { get; private set; }
    public DateTime Departure { get; private set; }
    public DateTime Arrival { get; private set; }
    public string BookingClass { get; private set; }
    public int LayoverDuration { get; private set; }
    public string FareBasis { get; private set; }
    public bool SubjectToGovernmentApproval { get; private set; }
    public string SegmentRef { get; private set; }
}