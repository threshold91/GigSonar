namespace GigSonar.Classes;

public class Venue
{
    private int _id;
    private string _externalId;
    private string _name;
    private string _url;
    private Location _LocationData;

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

    public string Url
    {
        get { return _url; }
        set { _url = value; }
    }

    public Location LocationData
    {
        get { return _LocationData; }
        set { _LocationData = value; }
    }
}