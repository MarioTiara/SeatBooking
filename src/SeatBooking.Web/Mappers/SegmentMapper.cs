using SeatBooking.Domain.SegmentAggregate;
using SeatBooking.Web.Models;
using System.Linq;

namespace SeatBooking.Web.Mappers;

public static class SegmentMapper
{
    public static Segment ToDomain(this SegmentDto dto)
    {
        // Defensive: ensure required fields are present
        if (dto == null) throw new ArgumentNullException(nameof(dto));
        if (dto.SegmentOfferInformation == null) throw new ArgumentNullException(nameof(dto.SegmentOfferInformation));
        if (dto.Flight == null) throw new ArgumentNullException(nameof(dto.Flight));

        // Handle StopAirports as List<string> (from List<object> in FlightDto)
        var stopAirports = dto.Flight.StopAirports?
            .Select(sa => sa?.ToString() ?? string.Empty)
            .Where(code => !string.IsNullOrWhiteSpace(code))
            .ToList() ?? new List<string>();

        var flightInfo = new FlightInfo(
            dto.Flight.FlightNumber ?? 0,
            dto.Flight.OperatingFlightNumber ?? 0,
            dto.Flight.AirlineCode ?? string.Empty,
            dto.Flight.OperatingAirlineCode ?? string.Empty,
            stopAirports,
            dto.Flight.DepartureTerminal ?? string.Empty,
            dto.Flight.ArrivalTerminal ?? string.Empty
        );

        return new Segment(
            dto.SegmentOfferInformation.FlightsMiles ?? 0,
            dto.SegmentOfferInformation.AwardFare ?? false,
            dto.Duration ?? 0,
            dto.CabinClass ?? string.Empty,
            dto.Equipment ?? string.Empty,
            flightInfo,
            dto.Origin ?? string.Empty,
            dto.Destination ?? string.Empty,
            DateTime.TryParse(dto.Departure, out var dep) ? dep : DateTime.MinValue,
            DateTime.TryParse(dto.Arrival, out var arr) ? arr : DateTime.MinValue,
            dto.BookingClass ?? string.Empty,
            dto.LayoverDuration ?? 0,
            dto.FareBasis ?? string.Empty,
            dto.SubjectToGovernmentApproval ?? false,
            dto.SegmentRef ?? string.Empty
        );
    }

     public static SegmentDto ToDto(this Segment segment)
    {
        return new SegmentDto(
            Type: "Segment", // or segment.GetType().Name if you want dynamic
            SegmentOfferInformation: new SegmentOfferInformationDto(
                FlightsMiles: segment.FlightsMiles,
                AwardFare: segment.AwardFare
            ),
            Duration: segment.Duration,
            CabinClass: segment.CabinClass,
            Equipment: segment.Equipment,
            Flight: segment.Flight == null ? null : new FlightDto(
                FlightNumber: segment.Flight.FlightNumber,
                OperatingFlightNumber: segment.Flight.OperatingFlightNumber,
                AirlineCode: segment.Flight.AirlineCode,
                OperatingAirlineCode: segment.Flight.OperatingAirlineCode,
                StopAirports: segment.Flight.StopAirportCodes?.Cast<object>().ToList(),
                DepartureTerminal: segment.Flight.DepartureTerminal,
                ArrivalTerminal: segment.Flight.ArrivalTerminal
            ),
            Origin: segment.OriginAirportCode,
            Destination: segment.DestinationAirportCode,
            Departure: segment.Departure.ToString("yyyy-MM-ddTHH:mm:ss"),
            Arrival: segment.Arrival.ToString("yyyy-MM-ddTHH:mm:ss"),
            BookingClass: segment.BookingClass,
            LayoverDuration: segment.LayoverDuration,
            FareBasis: segment.FareBasis,
            SubjectToGovernmentApproval: segment.SubjectToGovernmentApproval,
            SegmentRef: segment.SegmentRef
        );
    }
}