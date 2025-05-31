using SeatBooking.Domain.SegmentAggregate;
using SeatBooking.Web.Models;

namespace SeatBooking.Web.Mappers;

public static class SegmentMapper
{
    public static Segment ToDomain(this SegmentDto dto)
    {
        return new Segment(
            dto.SegmentOfferInformation.FlightsMiles,
            dto.SegmentOfferInformation.AwardFare,
            dto.Duration,
            dto.CabinClass,
            dto.Equipment,
            new FlightInfo(
                dto.Flight.FlightNumber,
                dto.Flight.OperatingFlightNumber,
                dto.Flight.AirlineCode,
                dto.Flight.OperatingAirlineCode,
                dto.Flight.StopAirports,
                dto.Flight.DepartureTerminal,
                dto.Flight.ArrivalTerminal
            ),
            dto.Origin,                
            dto.Destination,           
            DateTime.Parse(dto.Departure),
            DateTime.Parse(dto.Arrival),
            dto.BookingClass,
            dto.LayoverDuration,
            dto.FareBasis,
            dto.SubjectToGovernmentApproval,
            dto.SegmentRef
        );
    }
}