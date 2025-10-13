namespace GigSonar.DTOs.Ticketmaster.Search_events;

public record AccessDto
{
    public DateTime startDateTime { get; init; }
    public bool startApproximate { get; init; }
    public bool endApproximate { get; init; }
}
