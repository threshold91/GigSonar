namespace GigSonar.DTOs;

public record Dates
{
    public Access access { get; init; }
    public Start start { get; init; }
    public string timezone { get; init; }
    public Status status { get; init; }
    public bool spanMultipleDays { get; init; }
}