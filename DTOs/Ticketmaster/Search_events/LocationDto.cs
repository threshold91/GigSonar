namespace GigSonar.DTOs.Ticketmaster.Search_events;

public record LocationDto
{
    public string longitude { get; init; }
    public string latitude { get; init; }
}