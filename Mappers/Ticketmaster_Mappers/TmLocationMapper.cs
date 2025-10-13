using GigSonar.DTOs.Ticketmaster.Search_events;

namespace GigSonar.Mappers.Ticketmaster_Mappers;

public class TmLocationMapper
{
    public static Location Convert(Location location, CountryDto country, CityDto city, 
        AddressDto address, VenueDto venue)
    {
        return new Location
        {
            Latitude = location.Latitude,
            Longitude = location.Longitude,
            CountryCode = country.countryCode,
            City = city.name,
            Address = address.line1,
            PostalCode = venue.postalCode,
        };
    }
}