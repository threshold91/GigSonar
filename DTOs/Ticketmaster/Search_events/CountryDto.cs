namespace GigSonar.DTOs.Ticketmaster.Search_events;

public record CountryDto
{
    public string name { get; init; }
    public string countryCode { get; init; }
}