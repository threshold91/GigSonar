namespace GigSonar.DTOs.Ticketmaster_DTOs;

public record EmbeddedDto
{
    public List<EventDto> events { get; init; }
    public List<VenueDto> venues { get; init; }
    public List<AttractionDto> attractions { get; init; }
}