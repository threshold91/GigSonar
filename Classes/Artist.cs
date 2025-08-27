namespace GigSonar;

public class Artist
{
    private int _id;
    private string _name;
    private Genre _artistGenre;
    private string _spotifyLink;

    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public Genre ArtistGenre
    {
        get { return _artistGenre; }
        set { _artistGenre = value; }
    }

    public string SpotifyLink
    {
        get { return _spotifyLink; }
        set { _spotifyLink = value; }
    }
}