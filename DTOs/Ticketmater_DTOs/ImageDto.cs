namespace GigSonar.DTOs;

public record ImageDto
{
    public string ratio { get; init; }
    public string url { get; init; }
    public int width { get; init; }
    public int height { get; init; }
    public bool fallback { get; init; }
}