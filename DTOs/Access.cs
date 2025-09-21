namespace GigSonar.DTOs;

public record Access
{
    public DateTime startDateTime { get; init; }
    public bool startApproximate { get; init; }
    public bool endApproximate { get; init; }
}
