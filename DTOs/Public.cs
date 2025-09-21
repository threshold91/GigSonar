namespace GigSonar.DTOs;

public record Public
{
    public DateTime startDateTime { get; init; }
    public bool startTBD { get; init; }
    public bool startTBA { get; init; }
    public DateTime endDateTime { get; init; }
}