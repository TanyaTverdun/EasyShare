using EasyShare.Application.Common.Interfaces;
using EasyShare.Application.Features.Bookings.Extensions;
using EasyShare.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Bookings.Queries.GetUserBookingStats
{
    public class GetMyBookingStatsQueryHandler 
        : IRequestHandler<GetMyBookingStatsQuery, BookingStatsDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserContext _userContext;

        public GetMyBookingStatsQueryHandler(
            IApplicationDbContext context, 
            IUserContext userContext)
        {
            this._context = context;
            this._userContext = userContext;
        }

        public async Task<BookingStatsDto> Handle(
            GetMyBookingStatsQuery request, 
            CancellationToken cancellationToken)
        {
            var userId = this._userContext.UserId;

            var stats = await _context.Bookings
                .Where(b => b.UserId == userId)
                .GetStatsAsync(cancellationToken);

            return stats;
        }
    }
}
