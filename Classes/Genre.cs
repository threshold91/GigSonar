using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;

namespace GigSonar.Classes;
[Index(nameof(Id), IsUnique = false)]
[Index(nameof(ExternalId), IsUnique = true)]
public class Genre
{
    private int _id;
    private string _externalId;
    private string _name;

    public bool Validate()
    {
        
        if (string.IsNullOrWhiteSpace(_name))
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(_externalId))
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
}