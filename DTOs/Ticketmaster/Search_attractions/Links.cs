namespace GigSonar.DTOs.Ticketmaster.Search_attractions;

public record Links
{
    public Self self { get; init; }
    public List<Attraction> attractions { get; init; }
    public List<Venue> venues { get; init; }
    public First first { get; init; }
    public Next next { get; init; }
    public Last last { get; init; }
}