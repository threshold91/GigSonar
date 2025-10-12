using GigSonar.DTOs.Geocode_DTOs.Reverse_geocode_DTOs;
using RootDto = GigSonar.DTOs.Geocode_DTOs.Forward_geocode_DTOs.RootDto;

namespace GigSonar.Mappers.Geocode_Mappers;

public class GcLocationMapper
{
    public static Location Convert(RootDto rootDto, AddressDto  addressDto)
    {
        return new Location
        {
            Latitude = rootDto.Latitude,
            Longitude = rootDto.Longitude,
            CountryCode = addressDto.CountryCode,
            City = addressDto.City,
            Address = addressDto.Road,
            PostalCode = addressDto.Postcode
        };
    }
}