using System.Text.Json;
using GigSonar.Classes;
using GigSonar.DTOs.Ticketmaster.SearchAttractions;
using GigSonar.DTOs.Ticketmaster.SearchEvents;
using GigSonar.DTOs.Ticketmaster.SearchVenues;
using GigSonar.Mappers.Ticketmaster;
using Microsoft.Extensions.Configuration;
using Genre = GigSonar.Classes.Genre;
using Location = GigSonar.Classes.Location;
using Root = GigSonar.DTOs.Ticketmaster.SearchAttractions.Root;
using Venue = GigSonar.Classes.Venue;

namespace GigSonar;

class Program
{
    static readonly HttpClient client = new HttpClient();
    
    static async Task Main()
    {
        try
        {
            // Load config from appsettings.json
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Configurations/appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string ticketmasterKey = config["ApiKeys:Ticketmaster"];

            string url = "https://app.ticketmaster.com/discovery/v2/events.json"
                         + "?apikey=" + ticketmasterKey
                         + "&latlong=48.2082,16.3738&countryCode=AT&unit=km";
                         //+ "&countryCode=AT&latlong=48.2082,16.3738&unit=km&locale=en"//;
                         
            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                var testTmResponse = JsonSerializer.Deserialize<SearchEvents.Root>(responseBody);
                
                Event test = ConvertEvent(testTmResponse._embedded.events.First());
                
                Console.WriteLine(responseBody);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message: " + e.Message);
        }
    }

    public static Event ConvertEvent(DTOs.Ticketmaster.SearchEvents.SearchEvents.Event tmEvent)
    {
        var eventObject = new Event();
        eventObject.ExternalId = tmEvent.id;
        eventObject.Name = tmEvent.name;
        //event type - no such property in api response that would map to EventType enum
        eventObject.ArtistName = tmEvent._embedded.attractions.First().name;
        //genre - same as below
        //eventObject.Venue = ConvertVenue(tmEvent._embedded.venues.First()); is it possible te reuse already existing
        //mappers?
        eventObject.Start = tmEvent.dates.start.dateTime;
        //ends - festivals are represented as single objects that appear multiple times with  different start.dateTime
        //priceMin
        //priceMax
        //currency - price and currency is removed from ticketmaster discover api
        return eventObject;
    }
/*
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
   */ 
/*
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