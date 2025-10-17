using GigSonar.Classes;

namespace GigSonar.Mappers.Ticketmaster;

public class MapVenue
{
    public static Venue ConvertVenue(DTOs.Ticketmaster.SearchVenues.Venue tmVenue)
    {
        Venue venue = new Venue();
        venue.ExternalId = tmVenue.id;
        venue.Name = tmVenue.name;
        venue.Url = tmVenue.url;
        venue.LocationData = ConvertLocation(tmVenue);
        return venue;
    }

    private static Classes.Location ConvertLocation(DTOs.Ticketmaster.SearchVenues.Venue tmVenue)
    {
        var location = new Location();
        location.Latitude = tmVenue.location.latitude;
        location.Longitude = tmVenue.location.longitude;
        location.CountryCode = tmVenue.country.countryCode;
        location.City = tmVenue.city.name;
        location.Address = tmVenue.address.line1;
        location.PostalCode = tmVenue.postalCode;
        return location;
    }
}