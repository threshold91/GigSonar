namespace GigSonar;

public class User
{
    private Location _userLocation;
    public List<Notification> reminders { get; set; } = new List<Notification>();
    public List<Artist> favouriteArtists  { get; set; } = new List<Artist>();
    public List<Location> favouriteLocations { get; set; } = new List<Location>();
    public List<Genre> favouriteGenres { get; set; } = new List<Genre>();

    public Location UserLocation
    {
        get { return _userLocation; }
        set { _userLocation = value; }
    }
}