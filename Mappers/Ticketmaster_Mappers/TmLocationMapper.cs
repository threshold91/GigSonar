using GigSonar.DTOs.Ticketmaster.Search_events;

namespace GigSonar.Mappers.Ticketmaster_Mappers;

public class TmLocationMapper
{
    public static Location Convert(LocationDto location, CountryDto country, CityDto city, 
        AddressDto address, VenueDto venue)
    {
        return new Location
        {
            Latitude = location.latitude,
            Longitude = location.longitude,
            CountryCode = country.countryCode,
            City = city.name,
            Address = address.line1,
            PostalCode = venue.postalCode,
        };
    }
}