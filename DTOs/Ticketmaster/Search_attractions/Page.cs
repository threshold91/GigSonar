namespace GigSonar.DTOs.Ticketmaster.Search_attractions;

public record Page
{
    public int size { get; init; }
    public int totalElements { get; init; }
    public int totalPages { get; init; }
    public int number { get; init; }
}