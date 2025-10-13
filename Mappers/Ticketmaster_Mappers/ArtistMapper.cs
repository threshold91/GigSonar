using GigSonar.DTOs.Ticketmaster.Search_attractions;


namespace GigSonar.Mappers.Ticketmaster_Mappers;

public class ArtistMapper
{
    public static Artist Convert(Attraction attraction, Spotify spotify, Facebook facebook, Instagram instagram
    ,Homepage homepage)
    {
        return new Artist
        {
            ExternalId = attraction.Id,
            Name = attraction.Name,
            //figure out what to do with genre
            SpotifyLink = spotify.Url,
            FacebookLink = facebook.Url,
            InstagramLink = instagram.Url,
            ArtistHomepage = homepage.Url,
        };
    }
}