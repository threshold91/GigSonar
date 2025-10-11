namespace GigSonar.DTOs;

public record CountryDto
{
    public string name { get; init; }
    public string countryCode { get; init; }
}