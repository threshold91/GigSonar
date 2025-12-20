using GigSonar.Data;

namespace GigSonar;
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
using Root1 = GigSonar.DTOs.Ticketmaster.SearchEvents.SearchEvents.Root;
using Root2 = GigSonar.DTOs.Ticketmaster.SearchVenues.Root;
using Root3 = GigSonar.DTOs.Ticketmaster.SearchAttractions.Root;
using Venue = GigSonar.Classes.Venue;

using DtoEvent = GigSonar.DTOs.Ticketmaster.SearchEvents.SearchEvents.Event;
using DtoVenue = GigSonar.DTOs.Ticketmaster.SearchVenues.Venue;
using DtoAttraction = GigSonar.DTOs.Ticketmaster.SearchAttractions.Attraction;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
//using db = GigSonar.Data.GigSonarContext;





class Program
{
    static readonly HttpClient client = new HttpClient();
    
    static async Task Main()
    {
        //declare lists here so it is visible in both try & catch part
        List<Event> nonValidEvents = new List<Event>();
        List<Event> mappedEvents = new List<Event>();

        List<Venue> mappedVenues = new List<Venue>();
        List<Venue> nonValidVenues = new List<Venue>();

        List<Artist> mappedArtists = new List<Artist>();
        List<Artist> nonValidArtists = new List<Artist>();
        
            // Load config from appsettings.json
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Configurations/appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string ticketmasterKey = config["ApiKeys:Ticketmaster"];

            string url1 = "https://app.ticketmaster.com/discovery/v2/events.json"
                          + "?apikey=" + ticketmasterKey
                          + "&countryCode=AT"
                          + "&latlong=48.2082,16.3738"
                          + "&size=199";
            
            string url2 = "https://app.ticketmaster.com/discovery/v2/venues.json"
                          + "?apikey=" + ticketmasterKey
                          + "&countryCode=AT"
                          + "&latlong=48.2082,16.3738"
                          + "&size=199";
            
            string url3 = "https://app.ticketmaster.com/discovery/v2/attractions.json"
                          + "?apikey=" + ticketmasterKey
                          + "&countryCode=AT"
                          + "&latlong=48.2082,16.3738"
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
            
            // search venue
            using (HttpResponseMessage response = await client.GetAsync(url2))
            {
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                var testTmResponse = JsonSerializer.Deserialize<Root2>(responseBody);

                var json = await response.Content.ReadAsStringAsync();
                
                var testRoot = JsonConvert.DeserializeObject<Root2>(json);

                //Get dto venue from api, add them to list
                List<DtoVenue> dtoVenues = new List<DtoVenue>();
                if (testRoot != null)
                {
                    if (testRoot._embedded != null)
                    {
                        foreach (var dtoVenue in testRoot._embedded.venues)
                        {
                            if (dtoVenue != null)
                            {
                                dtoVenues.Add(dtoVenue);
                            }
                        }
                    }
                }
                
                //Convert dtoVenues to mappedVenues, add them to list
                
                foreach (var dtoVenue in dtoVenues)
                {
                    try
                    {
                        Venue mappedVenue = MapVenue.ConvertVenue(dtoVenue);
                        if (mappedVenue != null && mappedVenue.Validate())
                        {
                            mappedVenues.Add(mappedVenue);
                        }
                        else
                        {
                            nonValidVenues.Add(mappedVenue);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        nonValidVenues.Add(null);
                        continue;
                    }
                }
                
            }
            Console.WriteLine($"Number of valid venues is: {mappedVenues.Count}");
            Console.WriteLine($"Number of non valid venues is: {nonValidVenues.Count}!");
            
            // search artist
            using (HttpResponseMessage response = await client.GetAsync(url3))
            {
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                var testTmResponse = JsonSerializer.Deserialize<Root3>(responseBody);

                var json = await response.Content.ReadAsStringAsync();
                
                var testRoot = JsonConvert.DeserializeObject<Root3>(json);

                //Get dto venue from api, add them to list
                List<DtoAttraction> dtoAttractions = new List<DtoAttraction>();
                if (testRoot != null)
                {
                    if (testRoot._embedded != null)
                    {
                        foreach (var dtoAttraction in testRoot._embedded.attractions)
                        {
                            if (dtoAttraction != null)
                            {
                                dtoAttractions.Add(dtoAttraction);
                            }
                        }
                    }
                }
                //
                
                //Convert dtoEvents to mappedEvents, add them to list
                
                foreach (var dtoAttraction in dtoAttractions)
                {
                    try
                    {
                        Artist mappedArtist = MapArtist.ConvertArtist(dtoAttraction);
                        if (mappedArtist != null && mappedArtist.Validate())
                        {
                            mappedArtists.Add(mappedArtist);
                        }
                        else
                        {
                            nonValidArtists.Add(mappedArtist);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        nonValidArtists.Add(null);
                        continue;
                    }
                }
                
            }
            Console.WriteLine($"Number of valid artists is: {mappedArtists.Count}");
            Console.WriteLine($"Number of non valid artists is: {nonValidArtists.Count}!");
            using (var db = new GigSonarContext())
            {
                
                foreach (var venue in mappedVenues)
                {
                    var location = venue.LocationData;

                    if (!location.Validate())
                    {
                        Console.WriteLine($"Skipping location {location.ExternalId} – invalid");
                        continue;
                    }
                    // check & prevent duplicates by ExternalId
                    if (!db.Locations.Any(v => v.ExternalId == venue.LocationData.ExternalId))
                    {
                        db.Locations.Add(venue.LocationData);
                    }
                    
                    // check & prevent duplicates by ExternalId
                    if (!db.Venues.Any(v => v.ExternalId == venue.ExternalId))
                    {
                        db.Venues.Add(venue);
                    }
                }
                
                db.SaveChanges();

                var genres = db.Genres.ToList();
                foreach (var artist in mappedArtists)
                {
                    // check & prevent duplicates by ExternalId
                    if (!genres.Any(g => g.ExternalId == artist.Genre.ExternalId))
                    {
                        genres.Add(artist.Genre);
                    }
                    /*
                    // check & prevent duplicates by ExternalId
                    if (!db.Artists.Any(a => a.ExternalId == artist.ExternalId))
                    {
                        db.Artists.Add(artist);
                    } */
                db.Genres.AddRange(genres);
                db.SaveChanges();
                }
                /*
                foreach (var ev in mappedEvents)
                {
                    if (!db.Events.Any(e => e.ExternalId == ev.ExternalId))
                    {
                        db.Events.Add(ev);
                    }
                }
                
                db.SaveChanges();
                Console.WriteLine("Data saved to database."); */
            }
            
    }
}