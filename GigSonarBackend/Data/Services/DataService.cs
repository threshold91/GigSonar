
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
}