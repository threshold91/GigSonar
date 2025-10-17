using System.Text.Json;
using GigSonar.DTOs.Ticketmaster.SearchVenues;
using GigSonar.Mappers.Ticketmaster;
using Microsoft.Extensions.Configuration;
using Location = GigSonar.Classes.Location;
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

            string url = "https://app.ticketmaster.com/discovery/v2/venues.json"
                         + "?apikey=" + ticketmasterKey
                         + "&countryCode=AT&latlong=48.2082,16.3738&unit=km&locale=en";

            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                var testTmResponse = JsonSerializer.Deserialize<Root>(responseBody);
                
                Venue test = MapVenue.ConvertVenue(testTmResponse._embedded.venues.First());
                
                Console.WriteLine(responseBody);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message: " + e.Message);
        }
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