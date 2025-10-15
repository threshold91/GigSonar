namespace GigSonar.DTOs.Ticketmaster.Search_attractions;

public record Public
{
    public DateTime startDateTime { get; init; }
    public bool startTBD { get; init; }
    public bool startTBA { get; init; }
    public DateTime endDateTime { get; init; }
}