namespace GigSonar.DTOs.Ticketmaster.Search_events;

public record RootDto
{
    public EmbeddedDto EmbeddedDto { get; init; }
    public LinksDto LinksDto { get; init; }
    public PageDto PageDto { get; init; }
}