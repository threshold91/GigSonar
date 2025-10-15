namespace GigSonar.DTOs.Ticketmaster.Search_attractions;

public record Classification
{
    public bool primary { get; init; }
    public Segment segment { get; init; }
    public Genre genre { get; init; }
    public SubGenre subGenre { get; init; }
    public bool family { get; init; }
    public Type type { get; init; }
    public SubType subType { get; init; }
}