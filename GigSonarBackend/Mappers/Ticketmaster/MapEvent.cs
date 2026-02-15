using GigSonarBackend.Classes;

namespace GigSonarBackend.Mappers.Ticketmaster;

public class MapEvent
{
    public static Event ConvertEvent(DTOs.Ticketmaster.SearchEvents.SearchEvents.Event tmEvent)
    {
        var eventObject = new Event();
        eventObject.ExternalId = tmEvent.id;
        eventObject.Name = tmEvent.name;
        //eventObject.ExternalArtistId = tmEvent?._embedded?.attractions?.First()?.id;
        //eventObject.ArtistName = tmEvent?._embedded?.attractions?.First()?.name;
        eventObject.Performer = ConvertEventArtist(tmEvent?._embedded?.attractions?.First());
        eventObject.Genre = ConvertEventGenre(tmEvent?.classifications?.First());
        eventObject.SubGenre = ConvertEventSubGenre(tmEvent?.classifications?.First());
        eventObject.Venue = ConvertEventVenue(tmEvent?._embedded?.venues?.First());
        eventObject.Start = tmEvent.dates.start.dateTime;
        //below properties not yet mapped due to api limitations
        //event type - no such property in api response that would map to EventType enum
        //ends - festivals are represented as single objects that appear multiple times with  different start.dateTime
        //priceMin
        //priceMax
        //currency - price and currency is removed from ticketmaster discover api
        return eventObject;
    }
    
    private static Genre ConvertEventGenre(DTOs.Ticketmaster.SearchEvents.SearchEvents.Classification tmEventClassification)
    {
        var genre = new Genre();
        genre.ExternalId = tmEventClassification?.genre?.id;
        genre.Name = tmEventClassification?.genre?.name;
        return genre;
    }

    private static SubGenre ConvertEventSubGenre(
        DTOs.Ticketmaster.SearchEvents.SearchEvents.Classification tmEventSubGenre)
    {
        var subGenre = new SubGenre();
        subGenre.ExternalId = tmEventSubGenre?.subGenre?.id;
        subGenre.Name = tmEventSubGenre?.subGenre?.name;
        return subGenre;
    }

    public static Venue ConvertEventVenue(DTOs.Ticketmaster.SearchEvents.SearchEvents.Venue tmEventLocation)
    {
        var eventVenue = new Venue();
        eventVenue.ExternalId = tmEventLocation.id;
        eventVenue.Name = tmEventLocation?.name;
        eventVenue.Url = tmEventLocation?.url;
        eventVenue.LocationData = ConvertLocation(tmEventLocation);
        return eventVenue;
    }
    
    private static Classes.Location ConvertLocation(DTOs.Ticketmaster.SearchEvents.SearchEvents.Venue tmVenue)
    {
        var location = new Location();
        location.ExternalId = tmVenue.id;
        location.Latitude = tmVenue.location.latitude;
        location.Longitude = tmVenue.location.longitude;
        location.CountryCode = tmVenue.country.countryCode;
        location.City = tmVenue.city.name;
        location.Address = tmVenue?.address?.line1;
        location.PostalCode = tmVenue.postalCode;
        return location;
    }

    private static Artist ConvertEventArtist(DTOs.Ticketmaster.SearchEvents.SearchEvents.Attraction tmEventArtist)
    {
        var artist = new Artist();
        artist.ExternalId = tmEventArtist.id;
        artist.Name = tmEventArtist?.name;
        artist.Genre = ConvertEventGenre(tmEventArtist?.classifications?.First());
        artist.subGenre = ConvertEventSubGenre(tmEventArtist?.classifications?.First());
        artist.SpotifyLink = tmEventArtist?.externalLinks?.spotify?.ToString();
        artist.FacebookLink = tmEventArtist?.externalLinks?.facebook?.ToString();
        artist.InstagramLink = tmEventArtist?.externalLinks?.instagram?.ToString();
        artist.ArtistHomepage = tmEventArtist?.externalLinks?.homepage?.ToString();
        return artist;
    }
    
    private static Genre ConvertEventArtistGenre(DTOs.Ticketmaster.SearchEvents.SearchEvents.Attraction tmEventArtistClassification)
    {
        var genre = new Genre();
        genre.ExternalId = tmEventArtistClassification?.classifications?.First()?.genre?.id;
        genre.Name = tmEventArtistClassification?.classifications?.First()?.genre?.name;
        return genre;
    }

    private static SubGenre ConvertEventArtistSubGenre(DTOs.Ticketmaster.SearchEvents.SearchEvents.Attraction tmEventArtistClassification)
    {
        var subGenre = new SubGenre();
        subGenre.ExternalId = tmEventArtistClassification?.classifications?.First()?.subGenre?.id;
        subGenre.Name = tmEventArtistClassification?.classifications?.First()?.subGenre?.name;
        return subGenre;
    }
    
}
