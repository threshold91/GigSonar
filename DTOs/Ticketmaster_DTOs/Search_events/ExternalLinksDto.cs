namespace GigSonar.DTOs.Ticketmaster_DTOs.Search_events;

public record ExternalLinksDto
{
    public List<SpotifyDto> spotify { get; init; }
    public List<FacebookDto> facebook { get; init; }
    public List<InstagramDto> instagram { get; init; }
    public List<HomepageDto> homepage { get; init; }
}