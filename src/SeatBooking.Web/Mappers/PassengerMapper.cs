using SeatBooking.Domain.PassengerAggregate;
using SeatBooking.Domain.Shared;
using SeatBooking.Domain.Shared.ValueObjects;
using SeatBooking.Web.Models;

namespace SeatBooking.Web.Mappers;

public static class PassengerMapper
{
    public static Passenger ToDomain(this PassengerDto dto)
    {
        // Defensive: handle nulls for nested DTOs
        var address = dto.PassengerInfo?.Address is not null
            ? new Address(
                dto.PassengerInfo.Address.Street1 ?? string.Empty,
                dto.PassengerInfo.Address.Street2 ?? string.Empty,
                dto.PassengerInfo.Address.Postcode ?? string.Empty,
                dto.PassengerInfo.Address.State ?? string.Empty,
                dto.PassengerInfo.Address.City ?? string.Empty,
                dto.PassengerInfo.Address.Country ?? string.Empty,
                dto.PassengerInfo.Address.AddressType ?? string.Empty
            )
            : new Address("", "", "", "", "", "", "");

        var specialPreferences = dto.Preferences?.SpecialPreferences is not null
            ? new SpecialPreferences(
                dto.Preferences.SpecialPreferences.MealPreference ?? string.Empty,
                dto.Preferences.SpecialPreferences.SeatPreference ?? string.Empty,
                dto.Preferences.SpecialPreferences.SpecialRequests?
                    .OfType<string>()
                    .Select(v => new SpecialRequest { Value = v, PassengerId = dto.PassengerIndex ?? 0 })
                    .ToList() ?? new List<SpecialRequest>(),
                dto.Preferences.SpecialPreferences.SpecialServiceRequestRemarks?
                    .OfType<string>()
                    .Select(v => new SpecialServiceRequestRemark { Value = v, PassengerId = dto.PassengerIndex ?? 0 })
                    .ToList() ?? new List<SpecialServiceRequestRemark>()
            )
            : new SpecialPreferences("", "", new List<SpecialRequest>(), new List<SpecialServiceRequestRemark>());

        var documentInfo = (dto.DocumentInfo != null
            && !string.IsNullOrWhiteSpace(dto.DocumentInfo.IssuingCountry)
            && !string.IsNullOrWhiteSpace(dto.DocumentInfo.CountryOfBirth)
            && !string.IsNullOrWhiteSpace(dto.DocumentInfo.DocumentType)
            && !string.IsNullOrWhiteSpace(dto.DocumentInfo.Nationality))
            ? new DocumentInfo(
                dto.DocumentInfo.IssuingCountry,
                dto.DocumentInfo.CountryOfBirth,
                dto.DocumentInfo.DocumentType,
                dto.DocumentInfo.Nationality
            )
            : new DocumentInfo("UNKNOWN", "UNKNOWN", "UNKNOWN", "UNKNOWN");

        var frequentFlyers = dto.Preferences?.FrequentFlyer?
            .Where(ff => ff is not null)
            .Select(ff => new FrequentFlyer(
                ff.Airline ?? string.Empty,
                ff.Number ?? string.Empty,
                ff.TierNumber ?? 0
            ))
            .ToList() ?? new List<FrequentFlyer>();

        var emails = dto.PassengerInfo?.Emails?
            .Where(e => !string.IsNullOrWhiteSpace(e))
            .Select(e => new Email(e))
            .ToList() ?? new List<Email>();

        // Phones are List<object> in DTO, so convert to string if possible
        var phones = dto.PassengerInfo?.Phones?
            .OfType<string>()
            .Where(p => !string.IsNullOrWhiteSpace(p))
            .Select(p => new Phone(p))
            .ToList() ?? new List<Phone>();

        // Use DTO values for index and name number, fallback to safe defaults if missing
        var passengerIndex = dto.PassengerIndex ?? 0;
        var passengerNameNumber = dto.PassengerNameNumber ?? string.Empty;

        var passenger = new Passenger(
            passengerIndex,
            passengerNameNumber,
            dto.PassengerDetails?.FirstName ?? string.Empty,
            dto.PassengerDetails?.LastName ?? string.Empty,
            DateTime.TryParse(dto.PassengerInfo?.DateOfBirth, out var dob) ? dob : DateTime.MinValue,
            dto.PassengerInfo?.Gender ?? string.Empty,
            dto.PassengerInfo?.Type ?? string.Empty,
            address,
            specialPreferences,
            documentInfo
        );

        foreach (var email in emails)
            passenger.AddEmail(email);
        foreach (var phone in phones)
            passenger.AddPhone(phone);
        foreach (var ff in frequentFlyers)
            passenger.AddFrequentFlyer(ff);

        return passenger;
    }
}

