using GigSonar.Enums;

namespace GigSonar;

public class Event
{
    private int _id;
    private string _name;
    private EventType _type;
    private string _artist;
    private Genre _genre;
    private string _venue;
    private DateTime _starts;
    private DateTime _ends;
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

    public DateTime Starts
    {
        get { return _starts; }
        set { _starts = value; }
    }

    public DateTime Ends
    {
        get { return _ends; }
        set { _ends = value; }
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