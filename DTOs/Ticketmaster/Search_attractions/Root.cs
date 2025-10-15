namespace GigSonar.DTOs.Ticketmaster.Search_attractions;

public record Root
{
    public Embedded _embedded { get; init; }
    public Links _links { get; init; }
    public Page page { get; init; }
}