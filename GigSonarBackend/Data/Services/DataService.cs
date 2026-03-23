
using GigSonarBackend.Classes;
using GigSonarBackend.Data;
using GigSonarBackend.DTOs.Ticketmaster.SearchAttractions;
using GigSonarBackend.DTOs.Ticketmaster.SearchEvents;
using GigSonarBackend.DTOs.Ticketmaster.SearchVenues;
using GigSonarBackend.Mappers.Ticketmaster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DtoEvent = GigSonarBackend.DTOs.Ticketmaster.SearchEvents.SearchEvents.Event;
using DtoVenue = GigSonarBackend.DTOs.Ticketmaster.SearchVenues.Venue;
using DtoAttraction = GigSonarBackend.DTOs.Ticketmaster.SearchAttractions.Attraction;

using Newtonsoft.Json;
using Root = GigSonarBackend.DTOs.Ticketmaster.SearchVenues.Root;
using Venue = GigSonarBackend.Classes.Venue;

namespace GigSonarBackend.Data.Services;

public class DataService
{
    //URL builder - dictionaries
    private readonly string baseTicketmasterUrl = "https://app.ticketmaster.com/discovery/v2";
    
    private readonly Dictionary<string, string> ticketmasterEndpoints = new Dictionary<string, string>
    {
        { "events", "events.json" },
        { "venues", "venues.json" },
        { "artists", "attractions.json" }
    };

    private readonly Dictionary<string, Dictionary<string, string>> defaultTicketmasterParams = new
        Dictionary<string, Dictionary<string, string>>
        {
            {
                "events",
                new Dictionary<string, string>
                {
                    { "city", "Vienna" },
                    { "countryCode", "AT" },
                    { "segmentId", "KZFzniwnSyZfZ7v7nJ" },
                    { "size", "199" }
                }
            },
            {
                "venues",
                new Dictionary<string, string>
                {
                    { "city", "Vienna" },
                    { "countryCode", "AT" },
                    { "size", "199" }
                }
            },
            {
                "artists",
                new Dictionary<string, string>
                {
                    { "segmentId", "KZFzniwnSyZfZ7v7nJ" },
                    { "size", "199" }
                }
            }
        };
    
    //URL builder - method
    public string BuildTicketmasterUrl(string searchType, string keyword = null)
    {
        // Load config from appsettings.json
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("Configurations/appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        string ticketmasterKey = config["ApiKeys:Ticketmaster"];
        Dictionary<string, string> finalParameters = new Dictionary<string, string>();
        
        finalParameters.Add("apikey", ticketmasterKey);

        foreach (KeyValuePair<string, string> param in defaultTicketmasterParams[searchType])
        {
            finalParameters[param.Key] = param.Value;
        }
        
        //Optional keyword param
        if (!string.IsNullOrEmpty(keyword))
            finalParameters["keyword"] = keyword;

        string queryString = "";
        bool isFirstParameter = true;

        foreach (KeyValuePair<string, string> param in finalParameters)
        {
            if (!isFirstParameter)
                queryString = queryString + "&";
            
            queryString = queryString + param.Key + "=" + param.Value;
            
            isFirstParameter = false;
        }

        string url = baseTicketmasterUrl + "/" + ticketmasterEndpoints[searchType] + "?" + queryString;
        
        return url;
    }
    
    //Search Methods
    public List<Event> SearchEvents(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return new List<Event>();
        
        keyword = keyword.Trim().ToLower();

        using (var db = new GigSonarContext())
        {
            List<Event> allEvents = db.Events.ToList();
            List<Event> matchedEvents = new List<Event>();

            foreach (Event ev in allEvents)
            {
                if (ev.Name != null)
                {
                    string eventName = ev.Name.ToLower();
                    if (eventName.ToLower().Contains(keyword))
                        matchedEvents.Add(ev);
                }
            }
            
            var sorted = from e in matchedEvents
                orderby e.Name
                    select e;
            
            return sorted.ToList();
        }
    }

    public List<Venue> SearchVenues(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return new List<Venue>();
        
        keyword = keyword.Trim().ToLower();

        using (var db = new GigSonarContext())
        {
            List<Venue> allVenues = db.Venues.ToList();
            List<Venue> matchedVenues = new List<Venue>();

            foreach (Venue venue in allVenues)
            {
                if (venue.Name != null)
                {
                    string venueName = venue.Name.ToLower();
                    if (venueName.ToLower().Contains(keyword))
                        matchedVenues.Add(venue);
                }
            }
            
            var sorted = from venue in matchedVenues
                orderby venue.Name
                    select venue;
            
            return sorted.ToList();
        }
    }

    public List<Artist> SearchArtists(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return new List<Artist>();
        
        keyword = keyword.Trim().ToLower();

        using (var db = new GigSonarContext())
        {
            List<Artist> allArtists = db.Artists.ToList();
            List<Artist> matchedArtists = new List<Artist>();

            foreach (Artist artist in allArtists)
            {
                if (artist.Name != null)
                {
                    string artistName = artist.Name.ToLower();
                    if (artistName.ToLower().Contains(keyword))
                        matchedArtists.Add(artist);
                }
            }
            
            var sorted = from artist in matchedArtists
                orderby artist.Name
                    select artist;
            
            return sorted.ToList();
        }
    }
    
    //Deserialization
    public static async Task<T> GetAndDeserialize<T>(HttpClient httpClient, string url)
    {
        using (HttpResponseMessage response = await httpClient.GetAsync(url))
        {
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(responseBody);
            return result;
        }
    }

    public static List<TItem> ExtractItems<TRoot, TItem>(TRoot root,
        Func<TRoot, IEnumerable<TItem>> selector)
    {
        List<TItem> result = new List<TItem>();
        if (root == null)
            return result;
        
        IEnumerable<TItem> items = selector(root);
        
        if(items == null)
            return result;
        foreach (var item in items)
        {
            result.Add(item);
        }
        
        return result;
    }
    //Dto's extraction
    public static List<DtoEvent> ExtractEvents(SearchEvents.Root root)
    {
        List<DtoEvent> result = new List<DtoEvent>();
        
        if(root == null)
            return result;
        if(root._embedded == null)
            return result;
        if(root._embedded.events == null)
            return result;

        foreach (var dtoEvent in root._embedded.events)
        {
            if(dtoEvent != null)
                result.Add(dtoEvent);
        }
        
        return result;
    }

    public static List<DtoVenue> ExtractVenues(DTOs.Ticketmaster.SearchVenues.Root root)
    {
        List<DtoVenue> result = new List<DtoVenue>();
        
        if(root == null)
            return result;
        if(root._embedded ==  null)
            return result;
        if(root._embedded.venues == null)
            return result;

        foreach (var dtoVenue in root._embedded.venues)
        {
            if(dtoVenue != null)
                result.Add(dtoVenue);
        }
        
        return result;
    }

    public static List<DtoAttraction> ExtractAttractions(DTOs.Ticketmaster.SearchAttractions.Root root)
    {
        List<DtoAttraction> result = new List<DtoAttraction>();
        
        if(root == null)
            return result;
        if(root._embedded == null)
            return result;
        if(root._embedded.attractions == null)
            return result;

        foreach (var dtoAttraction in root._embedded.attractions)
        {
            if(dtoAttraction != null)
                result.Add(dtoAttraction);
        }
        
        return result;
    }
    
    //Dto's mapping and validation
    
    //Map and validate events
    public static List<Event> MapAndValidateEvents(List<DtoEvent> dtoEvents)
    {
        List<Event> validEvents = new List<Event>();
        List<Event> nonValidEvents = new List<Event>();
        if(dtoEvents == null)
            return validEvents;
        
        foreach (var dtoEvent in dtoEvents)
        {
            try
            {
                Event mappedEvent = MapEvent.ConvertEvent(dtoEvent);
                if (mappedEvent != null && mappedEvent.Validate())
                {
                    validEvents.Add(mappedEvent);
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
        
        return validEvents;
    }
    
    //Map and validate venues
    public static List<Venue> MapAndValidateVenues(List<DtoVenue> dtoVenues)
    {
        List<Venue> validVenues = new List<Venue>();
        List<Venue> nonValidVenues = new List<Venue>();

        if (dtoVenues == null)
            return validVenues;
        
        foreach (var dtoVenue in dtoVenues)
        {
            try
            {
                Venue mappedVenue = MapVenue.ConvertVenue(dtoVenue);
                        
                if (mappedVenue != null && mappedVenue.Validate() && mappedVenue.LocationData.Validate())
                {
                    mappedVenue.LocationData.PostalCode = GigSonarBackend.Classes.Location.SanitizePostalCode(mappedVenue.LocationData.PostalCode);
                    validVenues.Add(mappedVenue);
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
        
        return  validVenues;
    }
    
    //Map and validate attractions
    public static List<Artist> MapAndValidateArtists(List<DtoAttraction> dtoAttractions)
    {
        List<Artist> validArtists = new List<Artist>();
        List<Artist> nonValidArtists = new List<Artist>();
        
        foreach (var dtoAttraction in dtoAttractions)
        {
            try
            {
                Artist mappedArtist = MapArtist.ConvertArtist(dtoAttraction);
                if (mappedArtist != null && mappedArtist.Validate())
                {
                    validArtists.Add(mappedArtist);
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
        
        return validArtists;
    }
    
    //Save items to db
    
    //Save Venues to db
    public static void SaveNewVenues(List<Venue> mappedVenues)
    {
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
        }
    }
    
    //Save Artists to db
    public static void SaveNewArtists(List<Artist> mappedArtists)
    {
        using (var db = new GigSonarContext())
        {
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
                } 
            } 
                
            db.Artists.AddRange(newArtists);
            db.SaveChanges();
        }
    }
    
    //Save Events to db
    public static void SaveNewEvents(List<Event> mappedEvents)
    {
        using (var db = new GigSonarContext())
        {
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
                        var genreByExternalId = db.Genres.AsTracking()
                            .ToDictionary(g => g.ExternalId);
                        var subGenreByExternalId = db.SubGenres.AsTracking()
                            .ToDictionary(g => g.ExternalId);
                        
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
        }
    }
}