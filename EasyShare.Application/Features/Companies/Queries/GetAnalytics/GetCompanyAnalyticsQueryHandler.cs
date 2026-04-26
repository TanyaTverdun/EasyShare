using EasyShare.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Companies.Queries.GetAnalytics;

public class GetCompanyAnalyticsQueryHandler 
    : IRequestHandler<GetCompanyAnalyticsQuery, CompanyAnalyticsDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserContext _userContext;

    public GetCompanyAnalyticsQueryHandler(
        IApplicationDbContext context, 
        IUserContext userContext)
    {
        this._context = context;
        this._userContext = userContext;
    }

    public async Task<CompanyAnalyticsDto> Handle(
        GetCompanyAnalyticsQuery request, 
        CancellationToken cancellationToken)
    {
        var companyId = this._userContext.UserId;

        var query = this._context.Items
            .Where(i => i.CompanyId == companyId && i.IsActive);

        if (request.ItemTypeId.HasValue)
        {
            query = query.Where(i => i.TypeId == request.ItemTypeId);
        }

        var itemStats = await query
            .Select(i => new
            {
                i.Name,
                BookingsCount = i.Bookings
                    .Count(b => (!request.Year.HasValue || 
                                    b.CreatedAt.Year == request.Year) &&
                                (!request.Month.HasValue || 
                                    b.CreatedAt.Month == request.Month)),

                AverageRating = i.Bookings
                    .Where(b => (!request.Year.HasValue || 
                                    b.CreatedAt.Year == request.Year) &&
                                (!request.Month.HasValue || 
                                    b.CreatedAt.Month == request.Month))
                    .SelectMany(b => b.Reviews)
                    .Where(r => !r.IsOwner)
                    .Average(r => (double?)r.Rating) ?? 0.0
            })
            .ToListAsync(cancellationToken);

        return new CompanyAnalyticsDto
        {
            TopByBookings = itemStats
                .OrderByDescending(x => x.BookingsCount)
                .Take(7)
                .Select(x => new ItemStatDto 
                { 
                    Name = x.Name, 
                    Value = x.BookingsCount 
                })
                .ToList(),

            TopByRating = itemStats
                .Where(x => x.AverageRating > 0)
                .OrderByDescending(x => x.AverageRating)
                .Take(7)
                .Select(x => new ItemStatDto 
                { 
                    Name = x.Name, 
                    Value = Math.Round(x.AverageRating, 1) 
                })
                .ToList()
        };
    }
}
