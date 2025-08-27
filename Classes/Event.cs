namespace GigSonar;

public class Event
{
    private int _id;
    private string _name;
    private string _type;
    private string _artist;
    private Genre _eventGenre;
    private string _venue;
    private string _startsAt;
    private string _endsAt;
    private double _priceMin;
    private double _priceMax;
    private string _currency;

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

    public string Type
    {
        get { return _type; }
        set { _type = value; }
    }

    public Genre EventGenre
    {
        get { return _eventGenre; }
        set { _eventGenre = value; }
    }

    public string Artist
    {
        get { return _artist; }
        set { _artist = value; }
    }

    public string Venue
    {
        get { return _venue; }
        set { _venue = value; }
    }

    public string StartsAt
    {
        get { return _startsAt; }
        set { _startsAt = value; }
    }

    public string EndsAt
    {
        get { return _endsAt; }
        set { _endsAt = value; }
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