namespace GigSonar.DTOs;

public record LocationDto
{
    public string longitude { get; init; }
    public string latitude { get; init; }
}