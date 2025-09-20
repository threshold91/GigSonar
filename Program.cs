using System.Text.Json;

namespace GigSonar;

class Program
{
    static readonly HttpClient client = new HttpClient();
    
    static async Task Main()
    {
        // Call asynchronous network methods in a try/catch block to handle exceptions.
        try
        {
            using HttpResponseMessage response = await client.GetAsync("https://app.ticketmaster.com/discovery/v2/venues.json?apikey=DJDMqT8iBpI0dcivxf4TS0ketQfQv6Tp&countryCode=AT&latlong=48.2082,16.3738&unit=km&locale=en");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            // Above three lines can be replaced with new helper method below
            // string responseBody = await client.GetStringAsync(uri);

            List<Venue> venues = JsonSerializer.Deserialize<List<Venue>>(responseBody);
            
            Console.WriteLine(responseBody);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message :{0} ", e.Message);
        }
    }
}