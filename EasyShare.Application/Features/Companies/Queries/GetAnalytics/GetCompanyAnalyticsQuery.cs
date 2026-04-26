using MediatR;

namespace EasyShare.Application.Features.Companies.Queries.GetAnalytics;

public record GetCompanyAnalyticsQuery : IRequest<CompanyAnalyticsDto>
{
    public int? Year { get; init; }
    public int? Month { get; init; }
    public int? ItemTypeId { get; init; }
}
