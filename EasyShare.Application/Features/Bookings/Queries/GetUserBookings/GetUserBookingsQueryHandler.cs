using AutoMapper;
using AutoMapper.QueryableExtensions;
using EasyShare.Application.Common.Interfaces;
using EasyShare.Application.Features.Bookings.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Bookings.Queries.GetUserBookings;

public class GetMyBookingsQueryHandler 
    : IRequestHandler<GetMyBookingsQuery, List<BookingItemDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserContext _userContext;
    private readonly IMapper _mapper;

    public GetMyBookingsQueryHandler(
        IApplicationDbContext context, 
        IUserContext userContext,
        IMapper mapper)
    {
        this._context = context;
        this._userContext = userContext;
        this._mapper = mapper;
    }

    public async Task<List<BookingItemDto>> Handle(
        GetMyBookingsQuery request, 
        CancellationToken cancellationToken)
    {
        var userId = this._userContext.UserId;

        var bookings = await _context.Bookings
            .Where(b => b.UserId == userId)
            .ApplySearch(request.SearchTerm)
            .ApplyStatusFilter(request.StatusFilter)
            .ApplySorting(request.SortBy)
            .ProjectTo<BookingItemDto>(this._mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return bookings;
    }
}
