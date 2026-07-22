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
    private static readonly HttpClient Client = new();

    static async Task Main()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(
                "Configurations/appsettings.json",
                optional: false,
                reloadOnChange: true)
            .Build();

        string? ticketmasterKey = config["ApiKeys:Ticketmaster"];

        if (string.IsNullOrWhiteSpace(ticketmasterKey))
        {
            throw new InvalidOperationException(
                "Ticketmaster API key was not found.");
        }

        var dataService = new DataService();

        string eventsUrl =
            dataService.BuildTicketmasterUrl("events");

        string venuesUrl =
            dataService.BuildTicketmasterUrl("venues");

        string artistsUrl =
            dataService.BuildTicketmasterUrl(
                "artists",
                "Children of Bodom");

        // Test the new SearchEvents implementation
        List<Event> searchResults =
            await dataService.SearchEvents("children");

        Console.WriteLine(
            $"Found {searchResults.Count} events.");

        foreach (Event ev in searchResults)
        {
            Console.WriteLine(ev.Name);
        }

        // Events
        var eventRoot =
            await DataService.GetAndDeserialize<Root1>(
                Client,
                eventsUrl);

        var dtoEvents =
            DataService.ExtractEvents(eventRoot);

        List<Event> mappedEvents =
            DataService.MapAndValidateEvents(dtoEvents);

        Console.WriteLine(
            $"Number of valid events: {mappedEvents.Count}");

        // Venues
        var venueRoot =
            await DataService.GetAndDeserialize<Root2>(
                Client,
                venuesUrl);

        var dtoVenues =
            DataService.ExtractVenues(venueRoot);

        List<Venue> mappedVenues =
            DataService.MapAndValidateVenues(dtoVenues);

        Console.WriteLine(
            $"Number of valid venues: {mappedVenues.Count}");

        // Artists
        var artistRoot =
            await DataService.GetAndDeserialize<Root3>(
                Client,
                artistsUrl);

        var dtoAttractions =
            DataService.ExtractAttractions(artistRoot);

        List<Artist> mappedArtists =
            DataService.MapAndValidateArtists(dtoAttractions);

        Console.WriteLine(
            $"Number of valid artists: {mappedArtists.Count}");

        // Save referenced entities first
        await dataService.SaveNewVenues(mappedVenues);
        await dataService.SaveNewArtists(mappedArtists);

        // Save events after venues and artists
        await dataService.SaveNewEvents(mappedEvents);

        Console.WriteLine("Saving completed.");
    }
}