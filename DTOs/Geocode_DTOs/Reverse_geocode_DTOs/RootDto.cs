using Newtonsoft.Json;

namespace GigSonar.DTOs.Geocode_DTOs.Reverse_geocode_DTOs;

public record RootDto
{
    [JsonProperty("place_id")]
    public int PlaceId { get; init; }

    [JsonProperty("licence")]
    public string Licence { get; init; }

    [JsonProperty("osm_type")]
    public string OsmType { get; init; }

    [JsonProperty("osm_id")]
    public int OsmId { get; init; }

    [JsonProperty("lat")]
    public string Latitude { get; init; }

    [JsonProperty("lon")]
    public string Longitude { get; init; }

    [JsonProperty("display_name")]
    public string DisplayName { get; init; }

    [JsonProperty("address")]
    public AddressDto Address { get; init; }

    [JsonProperty("boundingbox")]
    public List<string> BoundingBox { get; init; }
}