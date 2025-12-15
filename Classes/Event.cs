namespace GigSonar.Classes;

public class Event
{
    private int _id;
    private string _externalId;
    private string _name;
    private EventType _type;
    private string _externalArtistId;
    private string _artistName;
    private Genre _genre;
    private Genre _subGenre;
    private Venue _venue;
    private DateTime _start;
    private DateTime _end;
    private double _priceMin;
    private double _priceMax;
    private string _currency;

    public bool Validate()
    {
        
        if (string.IsNullOrWhiteSpace(_name) && string.IsNullOrWhiteSpace(_artistName))
        {
            return false;
        }

        if (Venue is null)
        {
            return false;
        }

        if (Start == default)
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

    public EventType Type
    {
        get { return _type; }
        set { _type = value; }
    }

    public Genre Genre
    {
        get { return _genre; }
        set { _genre = value; }
    }

    public string ExternalArtistId
    {
        get { return _externalArtistId; }
        set { _externalArtistId = value; }
    }
    public string ArtistName
    {
        get { return _artistName; }
        set { _artistName = value; }
    }

    public Venue Venue
    {
        get { return _venue; }
        set { _venue = value; }
    }

    public DateTime Start
    {
        get { return _start; }
        set { _start = value; }
    }

    public DateTime End
    {
        get { return _end; }
        set { _end = value; }
    }

    public double PriceMin
    {
        get { return _priceMin; }
        set { _priceMin = value; }
    }

    public double PriceMax
    {
        get { return _priceMax; }
        set { _priceMax = value; }
    }

    public string Currency
    {
        get { return _currency; }
        set { _currency = value; }
    }
}