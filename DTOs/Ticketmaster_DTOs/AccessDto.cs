namespace GigSonar.DTOs.Ticketmaster_DTOs;

public record AccessDto
{
    public DateTime startDateTime { get; init; }
    public bool startApproximate { get; init; }
    public bool endApproximate { get; init; }
}
