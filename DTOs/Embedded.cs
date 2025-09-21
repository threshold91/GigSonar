namespace GigSonar.DTOs;

public record Embedded
{
    public List<Event> events { get; init; }
    public List<Venue> venues { get; init; }
    public List<Attraction> attractions { get; init; }
}