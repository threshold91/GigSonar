using GigSonar.Classes;

namespace GigSonar.Mappers.Ticketmaster;

public class MapEvent
{
    public static Event ConvertEvent(DTOs.Ticketmaster.SearchEvents.SearchEvents.Event tmEvent)
    {
        var eventObject = new Event();
        eventObject.ExternalId = tmEvent.id;
        eventObject.Name = tmEvent.name;
        eventObject.ExternalArtistId = tmEvent?._embedded?.attractions?.First()?.id;
        eventObject.ArtistName = tmEvent?._embedded?.attractions?.First()?.name;
        eventObject.Genre = ConvertEventGenre(tmEvent.classifications.First());
        eventObject.Venue = ConvertEventVenue(tmEvent._embedded.venues.First());
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
        genre.ExternalId = tmEventClassification.genre.id;
        genre.Name = tmEventClassification.genre.name;
      //  genre.SubGenre = ConvertEventSubGenre(tmEventClassification);
        return genre;
    }

    private static Genre ConvertEventSubGenre(
        DTOs.Ticketmaster.SearchEvents.SearchEvents.Classification tmEventSubGenre)
    {
        var subGenre = new Genre();
        subGenre.ExternalId = tmEventSubGenre.subGenre.id;
        subGenre.Name = tmEventSubGenre.subGenre.name;
        return subGenre;
    }

    public static Venue ConvertEventVenue(DTOs.Ticketmaster.SearchEvents.SearchEvents.Venue tmEventLocation)
    {
        var eventVenue = new Venue();
        eventVenue.ExternalId = tmEventLocation.id;
        eventVenue.Name = tmEventLocation.name;
        eventVenue.Url = tmEventLocation.url;
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
}