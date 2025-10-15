namespace GigSonar.DTOs.Ticketmaster.Search_attractions;

public record Attraction{
    public string href { get; init; }
    public string name { get; init; }
    public string type { get; init; }
    public string id { get; init; }
    public bool test { get; init; }
    public string url { get; init; }
    public string locale { get; init; }
    public ExternalLinks externalLinks { get; init; }
    public List<Image> images { get; init; }
    public List<Classification> classifications { get; init; }
    public UpcomingEvents upcomingEvents { get; init; }
    public Links _links { get; init; }
}