namespace GigSonar.DTOs.Ticketmaster.Search_events;

public record LinksDto
{
    public SelfDto SelfDto { get; init; }
    public List<AttractionDto> attractions { get; init; }
    public List<VenueDto> venues { get; init; }
    public FirstDto FirstDto { get; init; }
    public NextDto NextDto { get; init; }
    public LastDto LastDto { get; init; }
}