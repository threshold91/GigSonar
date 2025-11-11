using GigSonar.Classes;
using GigSonar.DTOs.Ticketmaster.SearchAttractions;
using GigSonar.DTOs.Ticketmaster.SearchEvents;
using GigSonar.DTOs.Ticketmaster.SearchVenues;
using GigSonar.Mappers.Ticketmaster;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Genre = GigSonar.Classes.Genre;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Location = GigSonar.Classes.Location;
using Root = GigSonar.DTOs.Ticketmaster.SearchEvents.SearchEvents.Root;
using Venue = GigSonar.Classes.Venue;

using DtoEvent = GigSonar.DTOs.Ticketmaster.SearchEvents.SearchEvents.Event;
namespace GigSonar;

class Program
{
    static readonly HttpClient client = new HttpClient();
    
    static async Task Main()
    {
        //declare lists here so it is visible in both try & catch part
        List<Event> nonValidEvents = new List<Event>();
        List<Event> mappedEvents = new List<Event>();
        
            // Load config from appsettings.json
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Configurations/appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string ticketmasterKey = config["ApiKeys:Ticketmaster"];

            string url = "https://app.ticketmaster.com/discovery/v2/events.json"
                         + "?apikey=" + ticketmasterKey
                         + "&latlong=48.2082,16.3738&countryCode=AT&unit=km&size=199";
                         //+ "&countryCode=AT&latlong=48.2082,16.3738&unit=km&locale=en"//;
            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                var testTmResponse = JsonSerializer.Deserialize<SearchEvents.Root>(responseBody);

                var json = await response.Content.ReadAsStringAsync();
                
                var testRoot = JsonConvert.DeserializeObject<SearchEvents.Root>(json);

                //Get dto events from api, add them to list
                List<DtoEvent> dtoEvents = new List<DtoEvent>();
                if (testRoot != null)
                {
                    if (testRoot._embedded != null)
                    {
                        foreach (var dtoEvent in testRoot._embedded.events)
                        {
                            if (dtoEvent != null)
                            {
                                dtoEvents.Add(dtoEvent);
                            }
                        }
                    }
                }
                //
                
                //Convert dtoEvents to mappedEvents, add them to list
                
                foreach (var dtoEvent in dtoEvents)
                {
                    try
                    {
                        Event mappedEvent = MapEvent.ConvertEvent(dtoEvent);
                        //mappedEvent.ArtistName = ""; // for testing purposes
                        if (mappedEvent != null)
                        {
                            mappedEvents.Add(mappedEvent);
                        }
                        else
                        {
                            nonValidEvents.Add(mappedEvent);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        nonValidEvents.Add(null);
                        continue;
                    }
                }
                
                //Event test = MapEvent.ConvertEvent(testTmResponse._embedded.events.First());
                
                //var Events = new List<Event>();
                
                /*
                foreach(var e in Events)
                    if (!e.Validate())
                        ;
                   */ 
                
                //Console.WriteLine(responseBody);
            }
            Console.WriteLine($"Number of valid events is: {mappedEvents.Count}");
            Console.WriteLine($"Number of non valid events is: {nonValidEvents.Count}!");
        }
            
    
    /*
    public static Event ConvertEvent(DTOs.Ticketmaster.SearchEvents.SearchEvents.Event tmEvent)
    {
        var eventObject = new Event();
        eventObject.ExternalId = tmEvent.id;
        eventObject.Name = tmEvent.name;
        eventObject.ArtistName = tmEvent._embedded.attractions.First().name;
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
        return genre;
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
        location.Latitude = tmVenue.location.latitude;
        location.Longitude = tmVenue.location.longitude;
        location.CountryCode = tmVenue.country.countryCode;
        location.City = tmVenue.city.name;
        location.Address = tmVenue.address.line1;
        location.PostalCode = tmVenue.postalCode;
        return location;
    }

    public static Artist ConvertArtist(DTOs.Ticketmaster.SearchAttractions.Attraction tmAttraction)
    {
        Artist artist = new Artist();
        artist.ExternalId = tmAttraction.id;
        artist.Name = tmAttraction.name;
        artist.SpotifyLink = tmAttraction.externalLinks.spotify.First().url;
        artist.FacebookLink = tmAttraction.externalLinks.facebook.First().url;
        artist.InstagramLink = tmAttraction.externalLinks.instagram.First().url;
        artist.ArtistHomepage = tmAttraction.externalLinks.homepage.First().url;
        artist.ArtistGenre = ConvertGenre(tmAttraction);
        return artist;
    }

    private static Genre ConvertGenre(DTOs.Ticketmaster.SearchAttractions.Attraction tmClassification)
    {
        var genre = new Genre();
        genre.ExternalId = tmClassification.classifications.First().genre.id;
        genre.Name = tmClassification.classifications.First().genre.name;
        return genre;
    }
    
    public static Venue ConvertVenue(DTOs.Ticketmaster.SearchVenues.Venue tmVenue)
    {
        Venue venue = new Venue();
        venue.ExternalId = tmVenue.id;
        venue.Name = tmVenue.name;
        venue.Url = tmVenue.url;
        venue.LocationData = ConvertLocation(tmVenue);
        return venue;
    }

    private static Classes.Location ConvertLocation(DTOs.Ticketmaster.SearchVenues.Venue tmVenue)
    {
        var location = new Location();
        location.Latitude = tmVenue.location.latitude;
        location.Longitude = tmVenue.location.longitude;
        location.CountryCode = tmVenue.country.countryCode;
        location.City = tmVenue.city.name;
        location.Address = tmVenue.address.line1;
        location.PostalCode = tmVenue.postalCode;
        return location;
    }
  */  
}