using GigSonar.DTOs.Ticketmaster_DTOs;

namespace GigSonar.Mappers.Ticketmaster_Mappers;

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