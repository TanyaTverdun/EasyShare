namespace EasyShare.Application.Features.Companies.Queries.GetAnalytics;

public record ItemStatDto
{
    public string Name { get; init; } = string.Empty;
    public double Value { get; init; }
}
