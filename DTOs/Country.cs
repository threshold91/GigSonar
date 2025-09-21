namespace GigSonar.DTOs;

public record Country
{
    public string name { get; init; }
    public string countryCode { get; init; }
}