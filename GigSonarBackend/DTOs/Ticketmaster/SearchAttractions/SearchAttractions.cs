namespace GigSonar.DTOs.Ticketmaster.SearchAttractions;

using Newtonsoft.Json;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Attraction
    {
        public string name { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public bool test { get; set; }
        public string url { get; set; }
        public string locale { get; set; }
        public ExternalLinks externalLinks { get; set; }
        public List<string> aliases { get; set; }
        public List<Image> images { get; set; }
        public List<Classification> classifications { get; set; }
        public UpcomingEvents upcomingEvents { get; set; }
        public Links _links { get; set; }
    }

    public class Classification
    {
        public bool primary { get; set; }
        public Segment segment { get; set; }
        public Genre genre { get; set; }
        public SubGenre subGenre { get; set; }
        public Type type { get; set; }
        public SubType subType { get; set; }
        public bool family { get; set; }
    }

    public class Embedded
    {
        public List<Attraction> attractions { get; set; }
    }

    public class ExternalLinks
    {
        public List<Youtube> youtube { get; set; }
        public List<Twitter> twitter { get; set; }
        public List<Itune> itunes { get; set; }
        public List<Lastfm> lastfm { get; set; }
        public List<Spotify> spotify { get; set; }
        public List<Facebook> facebook { get; set; }
        public List<Wiki> wiki { get; set; }
        public List<Instagram> instagram { get; set; }
        public List<Musicbrainz> musicbrainz { get; set; }
        public List<Homepage> homepage { get; set; }
    }

    public class Facebook
    {
        public string url { get; set; }
    }

    public class First
    {
        public string href { get; set; }
    }

    public class Genre
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Homepage
    {
        public string url { get; set; }
    }

    public class Image
    {
        public string ratio { get; set; }
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public bool fallback { get; set; }
        public string attribution { get; set; }
    }

    public class Instagram
    {
        public string url { get; set; }
    }

    public class Itune
    {
        public string url { get; set; }
    }

    public class Last
    {
        public string href { get; set; }
    }

    public class Lastfm
    {
        public string url { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }
        public First first { get; set; }
        public Next next { get; set; }
        public Last last { get; set; }
    }

    public class Musicbrainz
    {
        public string id { get; set; }
        public string url { get; set; }
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

    public class Segment
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }

    public class Spotify
    {
        public string url { get; set; }
    }

    public class SubGenre
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class SubType
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Twitter
    {
        public string url { get; set; }
    }

    public class Type
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class UpcomingEvents
    {
        public int ticketmaster { get; set; }
        public int _total { get; set; }
        public int _filtered { get; set; }
        public int? tmr { get; set; }
        public int? universe { get; set; }
        public int? crowder { get; set; }
    }

    public class Wiki
    {
        public string url { get; set; }
    }

    public class Youtube
    {
        public string url { get; set; }
    }

