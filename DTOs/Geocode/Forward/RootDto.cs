using Newtonsoft.Json;

namespace GigSonar.DTOs.Geocode.Forward;

public record RootDto
{
    [JsonProperty("place_id")]
    public int PlaceId { get; init; }

    [JsonProperty("licence")]
    public string Licence { get; init; }

    [JsonProperty("osm_type")]
    public string OsmType { get; init; }

    [JsonProperty("osm_id")]
    public object OsmId { get; init; }

    [JsonProperty("boundingbox")]
    public List<string> BoundingBox { get; init; }

    [JsonProperty("lat")]
    public string Latitude { get; init; }

    [JsonProperty("lon")]
    public string Longitude { get; init; }

    [JsonProperty("display_name")]
    public string DisplayName { get; init; }

    [JsonProperty("class")]
    public string Class { get; init; }

    [JsonProperty("type")]
    public string Type { get; init; }

    [JsonProperty("importance")]
    public double Importance { get; init; }
}