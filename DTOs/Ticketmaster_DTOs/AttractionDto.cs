namespace GigSonar.DTOs.Ticketmaster_DTOs;

public record AttractionDto
{
    public string href { get; init; }
    public string name { get; init; }
    public string type { get; init; }
    public string id { get; init; }
    public bool test { get; init; }
    public string url { get; init; }
    public string locale { get; init; }
    public ExternalLinksDto ExternalLinksDto { get; init; }
    public List<ImageDto> images { get; init; }
    public List<ClassificationDto> classifications { get; init; }
    public UpcomingEventsDto UpcomingEventsDto { get; init; }
    public LinksDto LinksDto { get; init; }
}