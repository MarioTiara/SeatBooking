namespace SeatBooking.Domain.SegmentAggregate;

public class FlightInfo
{
    protected FlightInfo() 
    {
        StopAirports = new List<string>().AsReadOnly();
    }
    public FlightInfo(
        int flightNumber,
        int operatingFlightNumber,
        string airlineCode,
        string operatingAirlineCode,
        IEnumerable<string> stopAirports,
        string departureTerminal,
        string arrivalTerminal)
    {
        if (string.IsNullOrWhiteSpace(airlineCode))
            throw new ArgumentException("Airline code cannot be null or empty.", nameof(airlineCode));
        if (string.IsNullOrWhiteSpace(operatingAirlineCode))
            throw new ArgumentException("Operating airline code cannot be null or empty.", nameof(operatingAirlineCode));
        if (stopAirports == null)
            throw new ArgumentNullException(nameof(stopAirports));
        if (string.IsNullOrWhiteSpace(departureTerminal))
            throw new ArgumentException("Departure terminal cannot be null or empty.", nameof(departureTerminal));
        if (string.IsNullOrWhiteSpace(arrivalTerminal))
            throw new ArgumentException("Arrival terminal cannot be null or empty.", nameof(arrivalTerminal));

        FlightNumber = flightNumber;
        OperatingFlightNumber = operatingFlightNumber;
        AirlineCode = airlineCode;
        OperatingAirlineCode = operatingAirlineCode;
        StopAirports = stopAirports.ToList().AsReadOnly();
        DepartureTerminal = departureTerminal;
        ArrivalTerminal = arrivalTerminal;
    }

    public int FlightNumber { get; }
    public int OperatingFlightNumber { get; }
    public string AirlineCode { get; }
    public string OperatingAirlineCode { get; }
    public IReadOnlyList<string> StopAirports { get; }
    public string DepartureTerminal { get; }
    public string ArrivalTerminal { get; }

    public override bool Equals(object? obj)
    {
        if (obj is not FlightInfo other) return false;
        return FlightNumber == other.FlightNumber &&
               OperatingFlightNumber == other.OperatingFlightNumber &&
               AirlineCode == other.AirlineCode &&
               OperatingAirlineCode == other.OperatingAirlineCode &&
               StopAirports.SequenceEqual(other.StopAirports) &&
               DepartureTerminal == other.DepartureTerminal &&
               ArrivalTerminal == other.ArrivalTerminal;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(
            FlightNumber,
            OperatingFlightNumber,
            AirlineCode,
            OperatingAirlineCode,
            string.Join(",", StopAirports),
            DepartureTerminal,
            ArrivalTerminal
        );
    }
}
