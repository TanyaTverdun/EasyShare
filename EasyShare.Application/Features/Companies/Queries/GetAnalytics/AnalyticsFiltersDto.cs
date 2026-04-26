namespace EasyShare.Application.Features.Companies.Queries.GetAnalytics;

public record AnalyticsFiltersDto
{
    public List<int> AvailableYears { get; init; } = new();
    public List<TypeFilterDto> AvailableTypes { get; init; } = new();
}
