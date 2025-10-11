namespace GigSonar.DTOs.Ticketmaster_DTOs;

public record DatesDto
{
    public AccessDto AccessDto { get; init; }
    public StartDto StartDto { get; init; }
    public string timezone { get; init; }
    public StatusDto StatusDto { get; init; }
    public bool spanMultipleDays { get; init; }
}