namespace GigSonar;

public class Location
{
    private double _latitude;
    private double _longitude;
    private string _countryCode;
    private string _city;
    private string _address;
    private string _postalCode;
    
    public double Latitude
    {
        get { return _latitude; }
        set { _latitude = value; }
    }

    public double Longitude
    {
        get { return _longitude; }
        set { _longitude = value; }
    }

    public string CountryCode
    {
        get { return _countryCode; }
        set { _countryCode = value; }
    }

    public string City
    {
        get { return _city; }
        set { _city = value; }
    }

    public string Address
    {
        get { return _address; }
        set { _address = value; }
    }

    public string PostalCode
    {
        get { return _postalCode; }
        set { _postalCode = value; }
    }
}