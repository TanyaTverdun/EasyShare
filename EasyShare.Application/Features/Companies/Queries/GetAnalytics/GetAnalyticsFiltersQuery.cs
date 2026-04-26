using MediatR;

namespace EasyShare.Application.Features.Companies.Queries.GetAnalytics;

public record GetAnalyticsFiltersQuery : IRequest<AnalyticsFiltersDto>;
