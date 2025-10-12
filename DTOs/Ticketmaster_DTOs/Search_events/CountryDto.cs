namespace GigSonar.DTOs.Ticketmaster_DTOs.Search_events;

public record CountryDto
{
    public string name { get; init; }
    public string countryCode { get; init; }
}