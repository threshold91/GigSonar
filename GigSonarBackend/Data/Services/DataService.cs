
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
    public static List<Event> MapAndValidateEvents(List<DtoEvent> dtoEvents)
    {
        List<Event> validEvents = new List<Event>();

        if (dtoEvents == null)
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                continue;
            }
        }
        
        return validEvents;
    }
}