namespace GigSonarBackend.Classes;

public class GeneralSearchResult
{
    public List<Event> Events { get; set; } = new List<Event>();
    public List<Venue> Venues { get; set; } = new List<Venue>();
    public List<Artist> Artists { get; set; } = new List<Artist>();

    public int TotalCount
    {
        get
        {
            return Events.Count + Venues.Count + Artists.Count;
        }
    }
}