namespace GigSonar.DTOs.Ticketmaster.SearchVenues;

using Newtonsoft.Json;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Address
    {
        public string line1 { get; set; }
    }

    public class City
    {
        public string name { get; set; }
    }

    public class Country
    {
        public string name { get; set; }
        public string countryCode { get; set; }
    }

    public class Embedded
    {
        public List<Venue> venues { get; set; }
    }

    public class First
    {
        public string href { get; set; }
    }

    public class Last
    {
        public string href { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }
        public First first { get; set; }
        public Next next { get; set; }
        public Last last { get; set; }
    }

    public class Location
    {
        public string longitude { get; set; }
        public string latitude { get; set; }
    }

    public class Next
    {
        public string href { get; set; }
    }

    public class Page
    {
        public int size { get; set; }
        public int totalElements { get; set; }
        public int totalPages { get; set; }
        public int number { get; set; }
    }

    public class Root
    {
        public Embedded _embedded { get; set; }
        public Links _links { get; set; }
        public Page page { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }

    public class UpcomingEvents
    {
        [JsonProperty("mfx-at")]
        public int mfxat { get; set; }
        public int _total { get; set; }
        public int _filtered { get; set; }
        public int? universe { get; set; }
    }

    public class Venue
    {
        public string name { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public bool test { get; set; }
        public string url { get; set; }
        public string locale { get; set; }
        public double distance { get; set; }
        public string units { get; set; }
        public string postalCode { get; set; }
        public string timezone { get; set; }
        public City city { get; set; }
        public Country country { get; set; }
        public Address address { get; set; }
        public Location location { get; set; }
        public UpcomingEvents upcomingEvents { get; set; }
        public Links _links { get; set; }
        public List<string> aliases { get; set; }
    }

