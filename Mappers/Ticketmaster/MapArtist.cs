using GigSonar.Classes;


namespace GigSonar.Mappers.Ticketmaster;

public class MapArtist
{
    public static Artist ConvertArtist(DTOs.Ticketmaster.SearchAttractions.Attraction tmAttraction)
    {
        Artist artist = new Artist();
        artist.ExternalId = tmAttraction.id;
        artist.Name = tmAttraction.name;
        artist.SpotifyLink = tmAttraction.externalLinks.spotify.First().url;
        artist.FacebookLink = tmAttraction.externalLinks.facebook.First().url;
        artist.InstagramLink = tmAttraction.externalLinks.instagram.First().url;
        artist.ArtistHomepage = tmAttraction.externalLinks.homepage.First().url;
        artist.ArtistGenre = ConvertGenre(tmAttraction);
        return artist;
    }

    private static Genre ConvertGenre(DTOs.Ticketmaster.SearchAttractions.Attraction tmClassification)
    {
        var genre = new Genre();
        genre.ExternalId = tmClassification.classifications.First().genre.id;
        genre.Name = tmClassification.classifications.First().genre.name;
        return genre;
    }
}