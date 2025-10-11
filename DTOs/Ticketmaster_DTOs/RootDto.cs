namespace GigSonar.DTOs.Ticketmaster_DTOs;

public record RootDto
{
    public EmbeddedDto EmbeddedDto { get; init; }
    public LinksDto LinksDto { get; init; }
    public PageDto PageDto { get; init; }
}