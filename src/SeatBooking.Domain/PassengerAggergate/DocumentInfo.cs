namespace SeatBooking.Domain.PassengerAggregate;

public class DocumentInfo
{
    public DocumentInfo(string issuingCountry, string countryOfBirth, string documentType, string nationality)
    {
        if (string.IsNullOrWhiteSpace(issuingCountry))
            throw new ArgumentException("Issuing country cannot be null or empty.", nameof(issuingCountry));
        if (string.IsNullOrWhiteSpace(countryOfBirth))
            throw new ArgumentException("Country of birth cannot be null or empty.", nameof(countryOfBirth));
        if (string.IsNullOrWhiteSpace(documentType))
            throw new ArgumentException("Document type cannot be null or empty.", nameof(documentType));
        if (string.IsNullOrWhiteSpace(nationality))
            throw new ArgumentException("Nationality cannot be null or empty.", nameof(nationality));

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
