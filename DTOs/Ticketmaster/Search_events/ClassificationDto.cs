namespace GigSonar.DTOs.Ticketmaster.Search_events;

public record ClassificationDto
{
    public bool primary { get; init; }
    public SegmentDto SegmentDto { get; init; }
    public GenreDto GenreDto { get; init; }
    public SubGenreDto SubGenreDto { get; init; }
    public bool family { get; init; }
    public TypeDto TypeDto { get; init; }
    public SubTypeDto SubTypeDto { get; init; }
}