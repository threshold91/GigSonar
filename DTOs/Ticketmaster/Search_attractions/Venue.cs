namespace GigSonar.DTOs.Ticketmaster.Search_attractions;

public record Venue
{
    public string href { get; init; }
    public string name { get; init; }
    public string type { get; init; }
    public string id { get; init; }
    public bool test { get; init; }
    public string url { get; init; }
    public string locale { get; init; }
    public double distance { get; init; }
    public string units { get; init; }
    public string postalCode { get; init; }
    public string timezone { get; init; }
    public City city { get; init; }
    public Country country { get; init; }
    public Address address { get; init; }
    public Location location { get; init; }
    public UpcomingEvents upcomingEvents { get; init; }
    public Links _links { get; init; }
}