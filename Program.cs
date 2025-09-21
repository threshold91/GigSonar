using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace GigSonar;

class Program
{
    static readonly HttpClient client = new HttpClient();
    
    static async Task Main()
    {
        try
        {
            // Load config from appsettings.json
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Configurations/appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string ticketmasterKey = config["ApiKeys:Ticketmaster"];

            string url = "https://app.ticketmaster.com/discovery/v2/venues.json"
                         + "?apikey=" + ticketmasterKey
                         + "&countryCode=AT&latlong=48.2082,16.3738&unit=km&locale=en";

            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message: " + e.Message);
        }
    }
}