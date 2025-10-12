using Newtonsoft.Json;

namespace GigSonar.DTOs.Geocode_DTOs.Reverse_geocode_DTOs;

public record AddressDto
{
    [JsonProperty("house_number")]
    public string HouseNumber { get; init; }

    [JsonProperty("road")]
    public string Road { get; init; }

    [JsonProperty("neighbourhood")]
    public string Neighbourhood { get; init; }

    [JsonProperty("suburb")]
    public string Suburb { get; init; }

    [JsonProperty("city_district")]
    public string CityDistrict { get; init; }

    [JsonProperty("city")]
    public string City { get; init; }

    [JsonProperty("ISO3166-2-lvl4")]
    public string ISO31662lvl4 { get; init; }

    [JsonProperty("postcode")]
    public string Postcode { get; init; }

    [JsonProperty("country")]
    public string Country { get; init; }

    [JsonProperty("country_code")]
    public string CountryCode { get; init; }
}