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

            string url1 = "https://app.ticketmaster.com/discovery/v2/events.json"
                         + "?apikey=" + ticketmasterKey
                         + "&size=199";
                         
            using (HttpResponseMessage response = await client.GetAsync(url1))
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
                        if (mappedEvent != null && mappedEvent.Validate())
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
                
            }
            Console.WriteLine($"Number of valid events is: {mappedEvents.Count}");
            Console.WriteLine($"Number of non valid events is: {nonValidEvents.Count}!");
        }
    
}