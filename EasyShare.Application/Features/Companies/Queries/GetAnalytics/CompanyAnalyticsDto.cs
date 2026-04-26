namespace EasyShare.Application.Features.Companies.Queries.GetAnalytics;

public record CompanyAnalyticsDto
{
    public List<ItemStatDto> TopByBookings { get; init; } = new();
    public List<ItemStatDto> TopByRating { get; init; } = new();
}
