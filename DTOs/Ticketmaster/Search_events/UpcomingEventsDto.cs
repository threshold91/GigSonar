using Newtonsoft.Json;

namespace GigSonar.DTOs.Ticketmaster.Search_events;

public record UpcomingEventsDto
{
    [JsonProperty("mfx-at")]
    public int mfxat { get; init; }
    public int _total { get; init; }
    public int _filtered { get; init; }

    [JsonProperty("mfx-cz")]
    public int mfxcz { get; init; }
    public int ticketnet { get; init; }

    [JsonProperty("mfx-nl")]
    public int mfxnl { get; init; }
    public int ticketmaster { get; init; }

    [JsonProperty("mfx-dk")]
    public int? mfxdk { get; init; }
}