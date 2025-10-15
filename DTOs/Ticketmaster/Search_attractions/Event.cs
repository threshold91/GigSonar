namespace GigSonar.DTOs.Ticketmaster.Search_attractions;

public record Event
{
    public string name { get; init; }
    public string type { get; init; }
    public string id { get; init; }
    public bool test { get; init; }
    public string url { get; init; }
    public string locale { get; init; }
    public List<Image> images { get; init; }
    public double distance { get; init; }
    public string units { get; init; }
    public Sales sales { get; init; }
    public Dates dates { get; init; }
    public List<Classification> classifications { get; init; }
    public Promoter promoter { get; init; }
    public List<Promoter> promoters { get; init; }
    public Seatmap seatmap { get; init; }
    public Ticketing ticketing { get; init; }
    public Links _links { get; init; }
    public Embedded _embedded { get; init; }
}