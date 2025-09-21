namespace GigSonar.DTOs;

public record ExternalLinks
{
    public List<Spotify> spotify { get; init; }
    public List<Facebook> facebook { get; init; }
    public List<Instagram> instagram { get; init; }
    public List<Homepage> homepage { get; init; }
}