namespace GigSonar.DTOs.Ticketmaster.Search_attractions;

using Newtonsoft.Json;
using System.Collections.Generic;

public record Attraction
{
    [JsonProperty("name")]
    public string Name { get; init; }

    [JsonProperty("type")]
    public string Type { get; init; }

    [JsonProperty("id")]
    public string Id { get; init; }

    [JsonProperty("test")]
    public bool Test { get; init; }

    [JsonProperty("url")]
    public string Url { get; init; }

    [JsonProperty("locale")]
    public string Locale { get; init; }

    [JsonProperty("externalLinks")]
    public ExternalLinks ExternalLinks { get; init; }

    [JsonProperty("aliases")]
    public List<string> Aliases { get; init; }

    [JsonProperty("images")]
    public List<Image> Images { get; init; }

    [JsonProperty("classifications")]
    public List<Classification> Classifications { get; init; }

    [JsonProperty("upcomingEvents")]
    public UpcomingEvents UpcomingEvents { get; init; }

    [JsonProperty("_links")]
    public Links Links { get; init; }
}

public record Classification
{
    [JsonProperty("primary")]
    public bool Primary { get; init; }

    [JsonProperty("segment")]
    public Segment Segment { get; init; }

    [JsonProperty("genre")]
    public Genre Genre { get; init; }

    [JsonProperty("subGenre")]
    public SubGenre SubGenre { get; init; }

    [JsonProperty("type")]
    public Type Type { get; init; }

    [JsonProperty("subType")]
    public SubType SubType { get; init; }

    [JsonProperty("family")]
    public bool Family { get; init; }
}

public record Embedded
{
    [JsonProperty("attractions")]
    public List<Attraction> Attractions { get; init; }
}

public record ExternalLinks
{
    [JsonProperty("youtube")]
    public List<Youtube> Youtube { get; init; }

    [JsonProperty("twitter")]
    public List<Twitter> Twitter { get; init; }

    [JsonProperty("itunes")]
    public List<Itune> Itunes { get; init; }

    [JsonProperty("facebook")]
    public List<Facebook> Facebook { get; init; }

    [JsonProperty("spotify")]
    public List<Spotify> Spotify { get; init; }

    [JsonProperty("instagram")]
    public List<Instagram> Instagram { get; init; }

    [JsonProperty("musicbrainz")]
    public List<Musicbrainz> Musicbrainz { get; init; }

    [JsonProperty("homepage")]
    public List<Homepage> Homepage { get; init; }
}

public record Facebook
{
    [JsonProperty("url")]
    public string Url { get; init; }
}

public record Genre
{
    [JsonProperty("id")]
    public string Id { get; init; }

    [JsonProperty("name")]
    public string Name { get; init; }
}

public record Homepage
{
    [JsonProperty("url")]
    public string Url { get; init; }
}

public record Image
{
    [JsonProperty("ratio")]
    public string Ratio { get; init; }

    [JsonProperty("url")]
    public string Url { get; init; }

    [JsonProperty("width")]
    public int Width { get; init; }

    [JsonProperty("height")]
    public int Height { get; init; }

    [JsonProperty("fallback")]
    public bool Fallback { get; init; }
}

public record Instagram
{
    [JsonProperty("url")]
    public string Url { get; init; }
}

public record Itune
{
    [JsonProperty("url")]
    public string Url { get; init; }
}

public record Links
{
    [JsonProperty("self")]
    public Self Self { get; init; }
}

public record Musicbrainz
{
    [JsonProperty("id")]
    public string Id { get; init; }

    [JsonProperty("url")]
    public string Url { get; init; }
}

public record Page
{
    [JsonProperty("size")]
    public int Size { get; init; }

    [JsonProperty("totalElements")]
    public int TotalElements { get; init; }

    [JsonProperty("totalPages")]
    public int TotalPages { get; init; }

    [JsonProperty("number")]
    public int Number { get; init; }
}

public record Root
{
    [JsonProperty("_embedded")]
    public Embedded Embedded { get; init; }

    [JsonProperty("_links")]
    public Links Links { get; init; }

    [JsonProperty("page")]
    public Page Page { get; init; }
}

public record Segment
{
    [JsonProperty("id")]
    public string Id { get; init; }

    [JsonProperty("name")]
    public string Name { get; init; }
}

public record Self
{
    [JsonProperty("href")]
    public string Href { get; init; }
}

public record Spotify
{
    [JsonProperty("url")]
    public string Url { get; init; }
}

public record SubGenre
{
    [JsonProperty("id")]
    public string Id { get; init; }

    [JsonProperty("name")]
    public string Name { get; init; }
}

public record SubType
{
    [JsonProperty("id")]
    public string Id { get; init; }

    [JsonProperty("name")]
    public string Name { get; init; }
}

public record Twitter
{
    [JsonProperty("url")]
    public string Url { get; init; }
}

public record Type
{
    [JsonProperty("id")]
    public string Id { get; init; }

    [JsonProperty("name")]
    public string Name { get; init; }
}

public record UpcomingEvents
{
    [JsonProperty("ticketnet")]
    public int Ticketnet { get; init; }

    [JsonProperty("mfx-se")]
    public int MfxSe { get; init; }

    [JsonProperty("mfx-be")]
    public int MfxBe { get; init; }

    [JsonProperty("mfx-ae")]
    public int MfxAe { get; init; }

    [JsonProperty("mfx-ch")]
    public int MfxCh { get; init; }

    [JsonProperty("tmr")]
    public int Tmr { get; init; }

    [JsonProperty("mfx-es")]
    public int MfxEs { get; init; }

    [JsonProperty("ticketmaster")]
    public int Ticketmaster { get; init; }

    [JsonProperty("mfx-de")]
    public int MfxDe { get; init; }

    [JsonProperty("mfx-it")]
    public int MfxIt { get; init; }

    [JsonProperty("crowder")]
    public int Crowder { get; init; }

    [JsonProperty("_total")]
    public int Total { get; init; }

    [JsonProperty("_filtered")]
    public int Filtered { get; init; }
}

public record Youtube
{
    [JsonProperty("url")]
    public string Url { get; init; }
}