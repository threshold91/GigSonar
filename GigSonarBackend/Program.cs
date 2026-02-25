using System.Diagnostics;
using GigSonarBackend.Classes;
using GigSonarBackend.Data;
using GigSonarBackend.Data.Services;
using GigSonarBackend.DTOs.Ticketmaster.SearchAttractions;
using GigSonarBackend.DTOs.Ticketmaster.SearchEvents;
using GigSonarBackend.Mappers.Ticketmaster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace GigSonarBackend;

using JsonSerializer = System.Text.Json.JsonSerializer;
using Location = Classes.Location;
using Root1 = DTOs.Ticketmaster.SearchEvents.SearchEvents.Root;
using Root2 = DTOs.Ticketmaster.SearchVenues.Root;
using Root3 = DTOs.Ticketmaster.SearchAttractions.Root;
using Venue = Classes.Venue;
using DtoAttraction = Attraction;

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
                          + "&segmentId=KZFzniwnSyZfZ7v7nJ"
                          + "&size=199";
            
            string url2 = "https://app.ticketmaster.com/discovery/v2/venues.json"
                          + "?apikey=" + ticketmasterKey
                          + "&countryCode=AT"
                          + "&latlong=48.2082,16.3738"
                          + "&size=199";
            
            string url3 = "https://app.ticketmaster.com/discovery/v2/attractions.json"
                          + "?apikey=" + ticketmasterKey
                          + "&segmentId=KZFzniwnSyZfZ7v7nJ"
                          + "&size=199";
            
            //Search Events
            var testRoot1 = await DataService.GetAndDeserialize<Root1>(client, url1);
            
            //Get dto events from api, add them to list
            var dtoEvents = DataService.ExtractEvents(testRoot1);
            
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
            
            Debug.WriteLine($"Number of valid events is: {mappedEvents.Count}");
            Console.WriteLine($"Number of non valid events is: {nonValidEvents.Count}!");
            
            // search venue
            var testRoot2 = await DataService.GetAndDeserialize<Root2>(client, url2);
            
            //Get dto venue from api, add them to list
            var dtoVenues = DataService.ExtractVenues(testRoot2);
            
            //Convert dtoVenues to mappedVenues, add them to list
            foreach (var dtoVenue in dtoVenues)
            {
                try
                {
                    Venue mappedVenue = MapVenue.ConvertVenue(dtoVenue);
                        
                    if (mappedVenue != null && mappedVenue.Validate() && mappedVenue.LocationData.Validate())
                    {
                        mappedVenue.LocationData.PostalCode = Location.SanitizePostalCode(mappedVenue.LocationData.PostalCode);
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
                
            
            Console.WriteLine($"Number of valid venues is: {mappedVenues.Count}");
            Console.WriteLine($"Number of non valid venues is: {nonValidVenues.Count}!");
            
            // search artist
            var testRoot3 = await DataService.GetAndDeserialize<Root3>(client, url3);
            
            //Get dto venue from api, add them to list
            List<DtoAttraction> dtoAttractions = new List<DtoAttraction>();
            if (testRoot3 != null)
            {
                if (testRoot3._embedded != null)
                {
                    foreach (var dtoAttraction in testRoot3._embedded.attractions)
                    {
                        if (dtoAttraction != null)
                        {
                            dtoAttractions.Add(dtoAttraction);
                        }
                    }
                }
            }
                
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
                
            
            Console.WriteLine($"Number of valid artists is: {mappedArtists.Count}");
            Console.WriteLine($"Number of non valid artists is: {nonValidArtists.Count}!");
            
            using (var db = new GigSonarContext())
            {
                var existingVenues = db.Venues.ToList();
                var newVenues = new List<Venue>();
                var locationByExternalId = db.Locations.AsTracking()
                    .ToDictionary(l => l.ExternalId);

                foreach (var venue in mappedVenues)
                {
                    var location = venue.LocationData;
                
                    if (!location.Validate())
                    {
                        Console.WriteLine($"Skipping location {location.ExternalId} – invalid");
                        continue;
                    }
                    
                    if (!existingVenues.Any(v => v.ExternalId == venue.ExternalId))
                    {
                        var locationExtId = venue.LocationData.ExternalId;

                        if (locationByExternalId.TryGetValue(locationExtId, out var existingLocation))
                        {
                            venue.LocationData = existingLocation; //points to existing row
                        }
                        else
                        {
                            db.Locations.Add(venue.LocationData); //new location
                            locationByExternalId[locationExtId] = venue.LocationData;
                        }
                        
                        existingVenues.Add(venue);
                        newVenues.Add(venue);
                    }
                    
                }
                db.Venues.AddRange(newVenues);
                db.SaveChanges();
                
                var exsistingArtists = db.Artists.ToList();
                var newArtists = new List<Artist>();
                var genreByExternalId = db.Genres.AsTracking()
                    .ToDictionary(g => g.ExternalId);
                var subGenreByExternalId = db.SubGenres.AsTracking()
                    .ToDictionary(g => g.ExternalId);
                
                foreach (var artist in mappedArtists)
                {
                    if (!exsistingArtists.Any(a => a.ExternalId == artist.ExternalId))
                    {
                        var genreExtId = artist.Genre.ExternalId;
                        var subGenreExtId = artist.subGenre.ExternalId;

                        if (genreByExternalId.TryGetValue(genreExtId, out var existingGenre))
                        {
                            artist.Genre = existingGenre;   // points to existing row
                        }
                        else
                        {
                            db.Genres.Add(artist.Genre);    // new genre
                            genreByExternalId[genreExtId] = artist.Genre;
                        }
                        
                        if (subGenreByExternalId.TryGetValue(subGenreExtId, out var existingSubGenre))
                        {
                            artist.subGenre = existingSubGenre;   // points to existing row
                        }
                        else
                        {
                            db.SubGenres.Add(artist.subGenre);    // new subgenre
                            subGenreByExternalId[subGenreExtId] = artist.subGenre;
                        }
                        
                        exsistingArtists.Add(artist);
                        newArtists.Add(artist);
                        //db.Artists.Add(artist);
                    } 
                } 
                
                db.Artists.AddRange(newArtists);
                db.SaveChanges();
                
                var existingEvents = db.Events.ToList();
                var newEvents = new List<Event>();
                var venueByExternalId = db.Venues.AsTracking()
                    .ToDictionary(v => v.ExternalId);
                var artistByExternalId = db.Artists.AsTracking()
                    .ToDictionary(a => a.ExternalId);
                foreach (var ev in mappedEvents)
                {
                    if (!existingEvents.Any(e => e.ExternalId == ev.ExternalId))
                    {
                        var genreExtId = ev.Genre.ExternalId;
                        var subGenreExtId = ev.SubGenre.ExternalId;
                        var venueExtId = ev.Venue.ExternalId;
                        var artistExtId = ev.Performer.ExternalId;
                        var eventArtistGenreExtId = ev.Performer.Genre.ExternalId;
                        var eventArtistSubGenreExtId = ev.Performer.subGenre.ExternalId;
                        
                        if (genreByExternalId.TryGetValue(genreExtId, out var existingGenre))
                        {
                            ev.Genre = existingGenre; // points to existing row
                        }
                        else
                        {
                            db.Genres.Add(ev.Genre); // new genre
                            genreByExternalId[genreExtId] = ev.Genre; 
                        }
                        
                        if (subGenreByExternalId.TryGetValue(subGenreExtId, out var existingSubGenre))
                        {
                            ev.SubGenre = existingSubGenre; // points to existing row
                        }
                        else
                        {
                            db.SubGenres.Add(ev.SubGenre); //new subgenre
                            subGenreByExternalId[subGenreExtId] = ev.SubGenre;
                        }

                        if (venueByExternalId.TryGetValue(venueExtId, out var existingVenue))
                        {
                            ev.Venue = existingVenue; // points to existing row
                        }
                        else
                        {
                            db.Venues.Add(ev.Venue); //new venue
                            venueByExternalId[venueExtId] = ev.Venue;
                        }

                        if (genreByExternalId.TryGetValue(eventArtistGenreExtId, out var duplicateGenre))
                        {
                            ev.Performer.Genre = duplicateGenre;    // points to existing row
                        }
                        else
                        {
                            genreByExternalId[eventArtistGenreExtId] = ev.Performer.Genre;  // new Genre
                        }

                        if (subGenreByExternalId.TryGetValue(eventArtistSubGenreExtId, out var duplicateSubGenre))
                        {
                            ev.Performer.subGenre = duplicateSubGenre;  // points to existing row
                        }
                        else
                        {
                            subGenreByExternalId[eventArtistSubGenreExtId] = ev.Performer.subGenre; // new subGenre
                        }

                        if (artistByExternalId.TryGetValue(artistExtId, out var exsistingArtist))
                        {
                            ev.Performer = exsistingArtist; // points to existing row
                        }
                        else
                        {
                            artistByExternalId[artistExtId] = ev.Performer; // new artist
                        }
                        existingEvents.Add(ev);
                        newEvents.Add(ev);
                    }
                }
                
                db.Events.AddRange(newEvents);
                db.SaveChanges();
                Console.WriteLine("Data saved to database."); 
            }
            
    }
}