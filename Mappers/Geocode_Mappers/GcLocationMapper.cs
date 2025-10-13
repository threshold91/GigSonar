using GigSonar.DTOs.Geocode.Reverse;
using RootDto = GigSonar.DTOs.Geocode.Forward.RootDto;

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