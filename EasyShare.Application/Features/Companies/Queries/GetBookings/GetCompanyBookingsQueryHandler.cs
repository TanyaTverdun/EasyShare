using AutoMapper;
using AutoMapper.QueryableExtensions;
using EasyShare.Application.Common.Interfaces;
using EasyShare.Application.Features.Companies.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Companies.Queries.GetBookings;

public class GetCompanyBookingsQueryHandler
: IRequestHandler<GetCompanyBookingsQuery, List<CompanyBookingDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserContext _userContext;
    private readonly IMapper _mapper;

    public GetCompanyBookingsQueryHandler(
        IApplicationDbContext context,
        IUserContext userContext,
        IMapper mapper)
    {
        this._context = context;
        this._userContext = userContext;
        this._mapper = mapper;
    }

    public async Task<List<CompanyBookingDto>> Handle(
        GetCompanyBookingsQuery request, 
        CancellationToken cancellationToken)
    {
        var companyId = this._userContext.UserId;

        var query = this._context.Bookings
            .Where(b => b.Item.CompanyId == companyId)
            .AsNoTracking()
            .Search(request.SearchTerm)
            .FilterByStatus(request.StatusFilter)
            .SortByNewest();

        return await query
            .ProjectTo<CompanyBookingDto>(this._mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
