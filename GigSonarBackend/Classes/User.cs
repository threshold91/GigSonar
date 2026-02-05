namespace GigSonar.Classes;

public class User
{
    private Location _userLocation;
    public List<Notification> Reminders { get; set; } = new List<Notification>();
    public List<Artist> FavouriteArtists  { get; set; } = new List<Artist>();
    public List<Location> FavouriteLocations { get; set; } = new List<Location>();
    public List<Genre> FavouriteGenres { get; set; } = new List<Genre>();

    public Location UserLocation
    {
        get { return _userLocation; }
        set { _userLocation = value; }
    }
}