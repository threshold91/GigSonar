namespace GigSonarBackend.Classes;

public class Artist
{
    private int _id;
    private string _externalId;
    private string _name;
    private Genre _genre;
    private SubGenre? _subGenre;
    private string? _spotifyLink;
    private string? _facebookLink;
    private string? _instagramLink;
    private string? _homepage;
    
    public bool Validate()
    {
        
        if (string.IsNullOrWhiteSpace(_name))
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(_genre.ToString()))
        {
            return false;
        }

        return true;
    }
 
    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public string ExternalId
    {
        get { return _externalId; }
        set { _externalId = value; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public Genre Genre
    {
        get { return _genre; }
        set { _genre = value; }
    }

    public SubGenre? subGenre
    {
        get {return  _subGenre;}
        set { _subGenre = value; }
    }

    public string? SpotifyLink
    {
        get { return _spotifyLink; }
        set { _spotifyLink = value; }
    }

    public string? FacebookLink
    {
        get { return _facebookLink; }
        set { _facebookLink = value; }
    }

    public string? InstagramLink
    {
        get { return _instagramLink; }
        set { _instagramLink = value; }
    }
    
    public string? ArtistHomepage
    {
        get { return _homepage; }
        set { _homepage = value; }
    }
}