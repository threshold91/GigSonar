using GigSonar.DTOs;

namespace GigSonar.Mappers;

public class LocationMapper
{
    public static Location Convert(LocationDto locationDto)
    {
        return new Location
        {
            Latitude = locationDto.latitude,
            Longitude = locationDto.longitude,
        };
    }
}