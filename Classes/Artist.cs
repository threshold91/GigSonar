namespace GigSonar;

public class Artist
{
    private int _id;
    private string _name;
    private string _genre;
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

    public string Genre
    {
        get { return _genre; }
        set { _genre = value; }
    }

    public string SpotifyLink
    {
        get { return _spotifyLink; }
        set { _spotifyLink = value; }
    }
}