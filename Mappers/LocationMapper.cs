using GigSonar.DTOs.Ticketmaster_DTOs;

namespace GigSonar.Mappers;

public class LocationMapper
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