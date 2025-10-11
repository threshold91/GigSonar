using GigSonar.DTOs;
using GigSonar.DTOs.Ticketmaster_DTOs;

namespace GigSonar.Mappers;

public class VenueMapper
{
    public static Venue Convert(VenueDto venueDto)
    {   
        return new Venue
        {
            ExternalId = venueDto.id,
            Name = venueDto.name,
            Url = venueDto.url,
        };
    }
}