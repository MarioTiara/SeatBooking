using SeatBooking.Domain.PassengerAggregate;
using SeatBooking.Domain.Shared;
using SeatBooking.Domain.Shared.ValueObjects;

using SeatBooking.Web.Models;

namespace SeatBooking.Web.Mappers;

public static class PassengerMapper
{
    public static Passenger ToDomain(this PassengerDto dto)
    {
        // Map Address
        var address = new Address(
            dto.PassengerInfo.Address.Street1,
            dto.PassengerInfo.Address.Street2,
            dto.PassengerInfo.Address.Postcode,
            dto.PassengerInfo.Address.State,
            dto.PassengerInfo.Address.City,
            dto.PassengerInfo.Address.Country,
            dto.PassengerInfo.Address.AddressType
        );

        // Map SpecialPreferences
        var specialPreferences = new SpecialPreferences(
            dto.Preferences.SpecialPreferences.MealPreference,
            dto.Preferences.SpecialPreferences.SeatPreference,
            dto.Preferences.SpecialPreferences.SpecialRequests
                .OfType<string>()
                .Select((v, i) => new SpecialRequest { Value = v, PassengerId = dto.PassengerIndex })
                .ToList(),
            dto.Preferences.SpecialPreferences.SpecialServiceRequestRemarks
                .OfType<string>()
                .Select((v, i) => new SpecialServiceRequestRemark { Value = v, PassengerId = dto.PassengerIndex })
                .ToList()
        );

        // Map DocumentInfo
        var documentInfo = new DocumentInfo(
            dto.DocumentInfo.IssuingCountry,
            dto.DocumentInfo.CountryOfBirth,
            dto.DocumentInfo.DocumentType,
            dto.DocumentInfo.Nationality
        );

        // Map FrequentFlyers
        var frequentFlyers = dto.Preferences.FrequentFlyer
            .Select(ff => new FrequentFlyer(ff.Airline, ff.Number, ff.TierNumber))
            .ToList();

        // Map Emails and Phones
        var emails = dto.PassengerInfo.Emails?.Select(e => new Email(e)).ToList() ?? new();
        var phones = dto.PassengerInfo.Phones?.Select(p => new Phone(p)).ToList() ?? new();

        var passenger = new Passenger(
            dto.PassengerDetails.FirstName,
            dto.PassengerDetails.LastName,
            DateTime.Parse(dto.PassengerInfo.DateOfBirth),
            dto.PassengerInfo.Gender,
            dto.PassengerInfo.Type,
            address,
            specialPreferences,
            documentInfo
        );

        // Add emails and phones
        foreach (var email in emails)
            passenger.AddEmail(email);
        foreach (var phone in phones)
            passenger.AddPhone(phone);

        // Add frequent flyers
        foreach (var ff in frequentFlyers)
            passenger.AddFrequentFlyer(ff);

        return passenger;
    }
}

