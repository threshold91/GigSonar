using System.Text.Json;
using GigSonar.Classes;
using GigSonar.DTOs.Ticketmaster.SearchAttractions;
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

            string url = "https://app.ticketmaster.com/discovery/v2/attractions.json"
                         + "?apikey=" + ticketmasterKey;
                         //+ "&countryCode=AT&latlong=48.2082,16.3738&unit=km&locale=en"//;

            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                var testTmResponse = JsonSerializer.Deserialize<Root>(responseBody);
                
                Artist test = ConvertArtist(testTmResponse._embedded.attractions.First(), 
                    testTmResponse._embedded.attractions.First().classifications.First());
                
                Console.WriteLine(responseBody);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message: " + e.Message);
        }
    }

    public static Artist ConvertArtist(DTOs.Ticketmaster.SearchAttractions.Attraction tmAttraction, 
        DTOs.Ticketmaster.SearchAttractions.Classification tmClassification)
    {
        Artist artist = new Artist();
        artist.ExternalId = tmAttraction.id;
        artist.Name = tmAttraction.name;
        //artist.SpotifyLink = tmAttraction.externalLinks.spotify;
        artist.ArtistGenre = ConvertGenre(tmClassification);
        return artist;
    }

    private static Genre ConvertGenre(DTOs.Ticketmaster.SearchAttractions.Classification tmClassification)
    {
        var genre = new Genre();
        genre.ExternalId = tmClassification.genre.id;
        genre.Name = tmClassification.genre.name;
        return genre;
    }
    
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