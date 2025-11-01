namespace GigSonar.Classes;

public class Genre
{
    private int _id;
    private string _externalId;
    private string _name;
    private Genre _subGenre;

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

    public Genre SubGenre
    {
        get { return _subGenre; }
        set { _subGenre = value; }
    }
}