namespace SeatBooking.Web.Models;

using System.Text.Json.Serialization;
using System.Collections.Generic;

// Root DTO
public record RootDto(
    [property: JsonPropertyName("seatsItineraryParts")] List<SeatsItineraryPartDto>? SeatsItineraryParts,
    [property: JsonPropertyName("selectedSeats")] List<object>? SelectedSeats
);

public record SeatsItineraryPartDto(
    [property: JsonPropertyName("segmentSeatMaps")] List<SegmentSeatMapDto>? SegmentSeatMaps
);

public record SegmentSeatMapDto(
    [property: JsonPropertyName("passengerSeatMaps")] List<PassengerSeatMapDto>? PassengerSeatMaps,
    [property: JsonPropertyName("segment")] SegmentDto? Segment
);

public record PassengerSeatMapDto(
    [property: JsonPropertyName("seatSelectionEnabledForPax")] bool? SeatSelectionEnabledForPax,
    [property: JsonPropertyName("seatMap")] SeatMapDto? SeatMap,
    [property: JsonPropertyName("passenger")] PassengerDto? Passenger
);

public record SeatMapDto(
    [property: JsonPropertyName("rowsDisabledCauses")] List<object>? RowsDisabledCauses,
    [property: JsonPropertyName("aircraft")] string? Aircraft,
    [property: JsonPropertyName("cabins")] List<CabinDto>? Cabins
);

public record CabinDto(
    [property: JsonPropertyName("deck")] string? Deck,
    [property: JsonPropertyName("seatColumns")] List<string>? SeatColumns,
    [property: JsonPropertyName("seatRows")] List<SeatRowDto>? SeatRows
);

public record SeatRowDto(
    [property: JsonPropertyName("rowNumber")] int? RowNumber,
    [property: JsonPropertyName("seatCodes")] List<string>? SeatCodes,
    [property: JsonPropertyName("seats")] List<SeatDto>? Seats
);

public record SeatDto(
    [property: JsonPropertyName("slotCharacteristics")] List<string>? SlotCharacteristics,
    [property: JsonPropertyName("storefrontSlotCode")] string? StorefrontSlotCode,
    [property: JsonPropertyName("available")] bool? Available,
    [property: JsonPropertyName("entitled")] bool? Entitled,
    [property: JsonPropertyName("feeWaived")] bool? FeeWaived,
    [property: JsonPropertyName("freeOfCharge")] bool? FreeOfCharge,
    [property: JsonPropertyName("originallySelected")] bool? OriginallySelected,
    [property: JsonPropertyName("code")] string? Code,
    [property: JsonPropertyName("designations")] List<string>? Designations,
    [property: JsonPropertyName("entitledRuleId")] string? EntitledRuleId,
    [property: JsonPropertyName("feeWaivedRuleId")] string? FeeWaivedRuleId,
    [property: JsonPropertyName("seatCharacteristics")] List<string>? SeatCharacteristics,
    [property: JsonPropertyName("limitations")] List<string>? Limitations,
    [property: JsonPropertyName("refundIndicator")] string? RefundIndicator,
    [property: JsonPropertyName("prices")] PriceAlternativesDto? Prices,
    [property: JsonPropertyName("taxes")] PriceAlternativesDto? Taxes,
    [property: JsonPropertyName("total")] PriceAlternativesDto? Total,
    [property: JsonPropertyName("rawSeatCharacteristics")] List<string>? RawSeatCharacteristics
);

public record PriceAlternativesDto(
    [property: JsonPropertyName("alternatives")] List<List<PriceDto>>? Alternatives
);

public record PriceDto(
    [property: JsonPropertyName("amount")] double? Amount,
    [property: JsonPropertyName("currency")] string? Currency
);

public record PassengerDto(
    [property: JsonPropertyName("passengerIndex")] int? PassengerIndex,
    [property: JsonPropertyName("passengerNameNumber")] string? PassengerNameNumber,
    [property: JsonPropertyName("passengerDetails")] PassengerDetailsDto? PassengerDetails,
    [property: JsonPropertyName("passengerInfo")] PassengerInfoDto? PassengerInfo,
    [property: JsonPropertyName("preferences")] PreferencesDto? Preferences,
    [property: JsonPropertyName("documentInfo")] DocumentInfoDto? DocumentInfo
);

public record PassengerDetailsDto(
    [property: JsonPropertyName("firstName")] string? FirstName,
    [property: JsonPropertyName("lastName")] string? LastName
);

public record PassengerInfoDto(
    [property: JsonPropertyName("dateOfBirth")] string? DateOfBirth,
    [property: JsonPropertyName("gender")] string? Gender,
    [property: JsonPropertyName("type")] string? Type,
    [property: JsonPropertyName("emails")] List<string>? Emails,
    [property: JsonPropertyName("phones")] List<object>? Phones,
    [property: JsonPropertyName("address")] AddressDto? Address
);

public record AddressDto(
    [property: JsonPropertyName("street1")] string? Street1,
    [property: JsonPropertyName("street2")] string? Street2,
    [property: JsonPropertyName("postcode")] string? Postcode,
    [property: JsonPropertyName("state")] string? State,
    [property: JsonPropertyName("city")] string? City,
    [property: JsonPropertyName("country")] string? Country,
    [property: JsonPropertyName("addressType")] string? AddressType
);

public record PreferencesDto(
    [property: JsonPropertyName("specialPreferences")] SpecialPreferencesDto? SpecialPreferences,
    [property: JsonPropertyName("frequentFlyer")] List<FrequentFlyerDto>? FrequentFlyer
);

public record SpecialPreferencesDto(
    [property: JsonPropertyName("mealPreference")] string? MealPreference,
    [property: JsonPropertyName("seatPreference")] string? SeatPreference,
    [property: JsonPropertyName("specialRequests")] List<object>? SpecialRequests,
    [property: JsonPropertyName("specialServiceRequestRemarks")] List<object>? SpecialServiceRequestRemarks
);

public record FrequentFlyerDto(
    [property: JsonPropertyName("airline")] string? Airline,
    [property: JsonPropertyName("number")] string? Number,
    [property: JsonPropertyName("tierNumber")] int? TierNumber
);

public record DocumentInfoDto(
    [property: JsonPropertyName("issuingCountry")] string? IssuingCountry,
    [property: JsonPropertyName("countryOfBirth")] string? CountryOfBirth,
    [property: JsonPropertyName("documentType")] string? DocumentType,
    [property: JsonPropertyName("nationality")] string? Nationality
);

public record SegmentDto(
    [property: JsonPropertyName("@type")] string? Type,
    [property: JsonPropertyName("segmentOfferInformation")] SegmentOfferInformationDto? SegmentOfferInformation,
    [property: JsonPropertyName("duration")] int? Duration,
    [property: JsonPropertyName("cabinClass")] string? CabinClass,
    [property: JsonPropertyName("equipment")] string? Equipment,
    [property: JsonPropertyName("flight")] FlightDto? Flight,
    [property: JsonPropertyName("origin")] string? Origin,
    [property: JsonPropertyName("destination")] string? Destination,
    [property: JsonPropertyName("departure")] string? Departure,
    [property: JsonPropertyName("arrival")] string? Arrival,
    [property: JsonPropertyName("bookingClass")] string? BookingClass,
    [property: JsonPropertyName("layoverDuration")] int? LayoverDuration,
    [property: JsonPropertyName("fareBasis")] string? FareBasis,
    [property: JsonPropertyName("subjectToGovernmentApproval")] bool? SubjectToGovernmentApproval,
    [property: JsonPropertyName("segmentRef")] string? SegmentRef
);

public record SegmentOfferInformationDto(
    [property: JsonPropertyName("flightsMiles")] int? FlightsMiles,
    [property: JsonPropertyName("awardFare")] bool? AwardFare
);

public record FlightDto(
    [property: JsonPropertyName("flightNumber")] int? FlightNumber,
    [property: JsonPropertyName("operatingFlightNumber")] int? OperatingFlightNumber,
    [property: JsonPropertyName("airlineCode")] string? AirlineCode,
    [property: JsonPropertyName("operatingAirlineCode")] string? OperatingAirlineCode,
    [property: JsonPropertyName("stopAirports")] List<object>? StopAirports,
    [property: JsonPropertyName("departureTerminal")] string? DepartureTerminal,
    [property: JsonPropertyName("arrivalTerminal")] string? ArrivalTerminal
);