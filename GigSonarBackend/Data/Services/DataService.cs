
using GigSonarBackend.Classes;
using GigSonarBackend.Data;
using GigSonarBackend.DTOs.Ticketmaster.SearchAttractions;
using GigSonarBackend.DTOs.Ticketmaster.SearchEvents;
using GigSonarBackend.DTOs.Ticketmaster.SearchVenues;
using GigSonarBackend.Mappers.Ticketmaster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
}