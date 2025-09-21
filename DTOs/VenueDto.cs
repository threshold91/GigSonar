namespace GigSonar.DTOs;

public record VenueDto
{
    public string href { get; init; }
    public string name { get; init; }
    public string type { get; init; }
    public string id { get; init; }
    public bool test { get; init; }
    public string url { get; init; }
    public string locale { get; init; }
    public double distance { get; init; }
    public string units { get; init; }
    public string postalCode { get; init; }
    public string timezone { get; init; }
    public CityDto CityDto { get; init; }
    public CountryDto CountryDto { get; init; }
    public AddressDto AddressDto { get; init; }
    public LocationDto LocationDto { get; init; }
    public UpcomingEventsDto UpcomingEventsDto { get; init; }
    public LinksDto LinksDto { get; init; }
}