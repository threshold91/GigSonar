namespace GigSonar.DTOs;

public record StartDto
{
    public string localDate { get; init; }
    public string localTime { get; init; }
    public DateTime dateTime { get; init; }
    public bool dateTBD { get; init; }
    public bool dateTBA { get; init; }
    public bool timeTBA { get; init; }
    public bool noSpecificTime { get; init; }
}