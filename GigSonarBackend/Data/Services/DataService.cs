
using GigSonarBackend.Classes;
using GigSonarBackend.Data;
using GigSonarBackend.DTOs.Ticketmaster.SearchAttractions;
using GigSonarBackend.DTOs.Ticketmaster.SearchEvents;
using GigSonarBackend.DTOs.Ticketmaster.SearchVenues;
using GigSonarBackend.Mappers.Ticketmaster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DtoEvent = GigSonarBackend.DTOs.Ticketmaster.SearchEvents.SearchEvents.Event;


using Newtonsoft.Json;
namespace GigSonarBackend.Data.Services;

public class DataService
{
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
            if (dtoEvent != null)
                result.Add(dtoEvent);
        }
        
        return result;
    }
}