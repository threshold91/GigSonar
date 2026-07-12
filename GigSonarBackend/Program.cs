using System.Diagnostics;
using GigSonarBackend.Classes;
using GigSonarBackend.Data.Services;
using Microsoft.Extensions.Configuration;

namespace GigSonarBackend;

using Root1 = DTOs.Ticketmaster.SearchEvents.SearchEvents.Root;
using Root2 = DTOs.Ticketmaster.SearchVenues.Root;
using Root3 = DTOs.Ticketmaster.SearchAttractions.Root;
using Venue = Classes.Venue;


class Program
{
    static readonly HttpClient client = new HttpClient();
    
    static async Task Main()
    {
            // Load config from appsettings.json
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Configurations/appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string ticketmasterKey = config["ApiKeys:Ticketmaster"];
            
            DataService dataService = new DataService();
            string url1 = dataService.BuildTicketmasterUrl("events");
            string url2 = dataService.BuildTicketmasterUrl("venues");
            string url3 = dataService.BuildTicketmasterUrl("artists","Children of bodom");
            
            //Search Events
            //var testRoot1 = await DataService.GetAndDeserialize<Root1>(client, url1);
            
            //Get dto events from api, add them to list
            //var dtoEvents = DataService.ExtractEvents(testRoot1);
            
            //Convert dtoEvents to mappedEvents, add them to list
            //List<Event> mappedEvents = DataService.MapAndValidateEvents(dtoEvents);
            
            List<Event> searchResults = await dataService.SearchEvents("children");

            Console.WriteLine($"Found {searchResults.Count} events.");

            foreach (Event ev in searchResults)
            {
                Console.WriteLine(ev.Name);
            }
            
            // search venue
            var testRoot2 = await DataService.GetAndDeserialize<Root2>(client, url2);
            
            //Get dto venue from api, add them to list
            var dtoVenues = DataService.ExtractVenues(testRoot2);
            
            //Convert dtoVenues to mappedVenues, add them to list
            List<Venue> mappedVenues = DataService.MapAndValidateVenues(dtoVenues);
            
            Console.WriteLine($"Number of valid venues is: {mappedVenues.Count}");
            
            // search artist
            var testRoot3 = await DataService.GetAndDeserialize<Root3>(client, url3);
            
            //Get dto venue from api, add them to list
            var dtoAttractions = DataService.ExtractAttractions(testRoot3);
            
            //Convert dtoAttractions to mappedArtists, add them to list
            var mappedArtists = DataService.MapAndValidateArtists(dtoAttractions);
            
            Console.WriteLine($"Number of valid artists is: {mappedArtists.Count}");
            
            DataService.SaveNewVenues(mappedVenues);
            DataService.SaveNewArtists(mappedArtists);
            //DataService.SaveNewEvents(mappedEvents);
        
    }
}