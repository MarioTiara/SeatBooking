using SeatBooking.Domain.Shared;

namespace SeatBooking.Domain.SegmentAggregate;

public class FlightInfo
{
    protected FlightInfo() 
    {
        StopAirports = new List<Airport>().AsReadOnly();
        StopAirportCodes = new List<string>().AsReadOnly();
    }
    public FlightInfo(
        int flightNumber,
        int operatingFlightNumber,
        string airlineCode,
        string operatingAirlineCode,
        IEnumerable<string> stopAirportCodes,
        string departureTerminal,
        string arrivalTerminal)
    {
        if (string.IsNullOrWhiteSpace(airlineCode))
            throw new ArgumentException("Airline code cannot be null or empty.", nameof(airlineCode));
        if (string.IsNullOrWhiteSpace(operatingAirlineCode))
            throw new ArgumentException("Operating airline code cannot be null or empty.", nameof(operatingAirlineCode));
        if (stopAirportCodes == null)
            throw new ArgumentNullException(nameof(stopAirportCodes));
        if (string.IsNullOrWhiteSpace(departureTerminal))
            throw new ArgumentException("Departure terminal cannot be null or empty.", nameof(departureTerminal));
        if (string.IsNullOrWhiteSpace(arrivalTerminal))
            throw new ArgumentException("Arrival terminal cannot be null or empty.", nameof(arrivalTerminal));

        FlightNumber = flightNumber;
        OperatingFlightNumber = operatingFlightNumber;
        AirlineCode = airlineCode;
        OperatingAirlineCode = operatingAirlineCode;
        StopAirportCodes = stopAirportCodes?.ToList().AsReadOnly() ?? new List<string>().AsReadOnly();
        DepartureTerminal = departureTerminal;
        ArrivalTerminal = arrivalTerminal;
    }

    public int Id { get; private set; }
    public int FlightNumber { get; private set; }
    public int OperatingFlightNumber { get; private set; }
    public string AirlineCode { get; private set; }
    public string OperatingAirlineCode { get; private set; }
    public IReadOnlyList<Airport> StopAirports { get; private set; }
    public IReadOnlyList<string> StopAirportCodes { get; private set; }
    public string DepartureTerminal { get; private set; }
    public string ArrivalTerminal { get; private set; }

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
