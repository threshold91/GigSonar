namespace GigSonar.DTOs;

public record EventDto
{
    public string name { get; init; }
    public string type { get; init; }
    public string id { get; init; }
    public bool test { get; init; }
    public string url { get; init; }
    public string locale { get; init; }
    public List<ImageDto> images { get; init; }
    public double distance { get; init; }
    public string units { get; init; }
    public SalesDto SalesDto { get; init; }
    public DatesDto DatesDto { get; init; }
    public List<ClassificationDto> classifications { get; init; }
    public PromoterDto PromoterDto { get; init; }
    public List<PromoterDto> promoters { get; init; }
    public SeatmapDto SeatmapDto { get; init; }
    public TicketingDto TicketingDto { get; init; }
    public LinksDto LinksDto { get; init; }
    public EmbeddedDto EmbeddedDto { get; init; }
}