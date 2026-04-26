using AutoMapper;
using AutoMapper.QueryableExtensions;
using EasyShare.Application.Common.Exceptions;
using EasyShare.Application.Common.Interfaces;
using EasyShare.Application.Common.Models;
using EasyShare.Application.Features.Bookings.Extensions;
using MediatR;

namespace EasyShare.Application.Features.Bookings.Queries.GetUserBookings;

public class GetMyBookingsQueryHandler 
    : IRequestHandler<GetUserBookingsQuery, PagedResult<BookingItemDto>>
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

    public async Task<PagedResult<BookingItemDto>> Handle(
        GetUserBookingsQuery request,
        CancellationToken cancellationToken)
    {
        var userId = this._userContext.UserId;

        return await _context.Bookings
            .Where(b => b.UserId == userId)
            .ApplySearch(request.SearchTerm)
            .ApplyStatusFilter(request.StatusFilter)
            .ApplySorting(request.SortBy)
            .ProjectTo<BookingItemDto>(this._mapper.ConfigurationProvider) // Спочатку мапимо
            .PaginatedListAsync(
                request.Page, 
                request.PageSize, 
                cancellationToken);
    }
}
