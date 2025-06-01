namespace SeatBooking.Domain.PassengerAggregate;

public class DocumentInfo
{
    public DocumentInfo(string issuingCountry, string countryOfBirth, string documentType, string nationality)
    {
        IssuingCountry = issuingCountry;
        CountryOfBirth = countryOfBirth;
        DocumentType = documentType;
        Nationality = nationality;
    }
    protected DocumentInfo() 
    {
    }
public int Id { get; private set; }
    public string IssuingCountry { get; }
    public string CountryOfBirth { get; }
    public string DocumentType { get; }
    public string Nationality { get; }

    public override bool Equals(object? obj)
    {
        if (obj is not DocumentInfo other) return false;

        return IssuingCountry == other.IssuingCountry &&
               CountryOfBirth == other.CountryOfBirth &&
               DocumentType == other.DocumentType &&
               Nationality == other.Nationality;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(IssuingCountry, CountryOfBirth, DocumentType, Nationality);
    }
}
