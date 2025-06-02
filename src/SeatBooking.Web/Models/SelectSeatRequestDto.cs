using System.Text.Json.Serialization;

namespace SeatBooking.Web.Models;

public record SelectSeatRequestDto(
    [property: JsonPropertyName("seatSlotCode")] string SeatSlotCode,
    [property: JsonPropertyName("aircraftCode")] string AircraftCode,
    [property: JsonPropertyName("passengerNameNumber")] string PassengerNameNumber
);