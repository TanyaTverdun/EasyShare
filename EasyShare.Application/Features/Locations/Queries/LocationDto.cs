namespace EasyShare.Application.Features.Locations.Queries;

public record LocationDto
{
    public string City { get; init; }
    public string Street { get; init; }
    public int Building { get; init; }
}
