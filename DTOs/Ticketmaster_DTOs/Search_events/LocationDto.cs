namespace GigSonar.DTOs.Ticketmaster_DTOs.Search_events;

public record LocationDto
{
    public string longitude { get; init; }
    public string latitude { get; init; }
}