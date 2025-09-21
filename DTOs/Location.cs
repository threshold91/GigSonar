namespace GigSonar.DTOs;

public record Location
{
    public string longitude { get; init; }
    public string latitude { get; init; }
}