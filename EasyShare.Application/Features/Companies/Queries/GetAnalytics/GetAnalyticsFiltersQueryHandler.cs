using EasyShare.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Companies.Queries.GetAnalytics;

public class GetAnalyticsFiltersQueryHandler 
    : IRequestHandler<GetAnalyticsFiltersQuery, AnalyticsFiltersDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserContext _userContext;

    public GetAnalyticsFiltersQueryHandler(
        IApplicationDbContext context, 
        IUserContext userContext)
    {
        this._context = context;
        this._userContext = userContext;
    }

    public async Task<AnalyticsFiltersDto> Handle(
        GetAnalyticsFiltersQuery request, 
        CancellationToken cancellationToken)
    {
        var companyId = this._userContext.UserId;

        var years = await this._context.Bookings
            .Where(b => b.Item.CompanyId == companyId)
            .Select(b => b.CreatedAt.Year)
            .Distinct()
            .OrderByDescending(y => y)
            .ToListAsync(cancellationToken);

        var types = await this._context.Items
            .Where(i => i.CompanyId == companyId && i.IsActive)
            .Select(i => i.ItemType)
            .Distinct()
            .Select(t => new TypeFilterDto
            {
                Id = t.Id,
                Name = t.Name
            })
            .OrderBy(t => t.Name)
            .ToListAsync(cancellationToken);

        return new AnalyticsFiltersDto
        {
            AvailableYears = years,
            AvailableTypes = types
        };
    }
}
