namespace GigSonar.DTOs;

public record PageDto
{
    public int size { get; init; }
    public int totalElements { get; init; }
    public int totalPages { get; init; }
    public int number { get; init; }
}