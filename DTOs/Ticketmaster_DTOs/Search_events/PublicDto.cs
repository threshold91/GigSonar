namespace GigSonar.DTOs.Ticketmaster_DTOs.Search_events;

public record PublicDto
{
    public DateTime startDateTime { get; init; }
    public bool startTBD { get; init; }
    public bool startTBA { get; init; }
    public DateTime endDateTime { get; init; }
}