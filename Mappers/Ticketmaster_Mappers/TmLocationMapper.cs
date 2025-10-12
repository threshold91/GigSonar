using GigSonar.DTOs.Ticketmaster_DTOs;
using GigSonar.DTOs.Ticketmaster_DTOs.Search_events;

namespace GigSonar.Mappers.Ticketmaster_Mappers;

public class TmLocationMapper
{
    public static Location Convert(LocationDto locationDto, CountryDto countryDto, CityDto cityDto, 
        AddressDto addressDto, VenueDto venueDto)
    {
        return new Location
        {
            Latitude = locationDto.latitude,
            Longitude = locationDto.longitude,
            CountryCode = countryDto.countryCode,
            City = cityDto.name,
            Address = addressDto.line1,
            PostalCode =venueDto.postalCode,
        };
    }
}