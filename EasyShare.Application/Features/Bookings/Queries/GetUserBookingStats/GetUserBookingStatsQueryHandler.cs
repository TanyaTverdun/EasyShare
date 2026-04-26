using EasyShare.Application.Common.Interfaces;
using EasyShare.Application.Features.Bookings.Extensions;
using MediatR;

namespace EasyShare.Application.Features.Bookings.Queries.GetUserBookingStats
{
    public class GetUserBookingStatsQueryHandler 
        : IRequestHandler<GetUserBookingStatsQuery, BookingStatsDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserContext _userContext;

        public GetUserBookingStatsQueryHandler(
            IApplicationDbContext context, 
            IUserContext userContext)
        {
            this._context = context;
            this._userContext = userContext;
        }

        public async Task<BookingStatsDto> Handle(
            GetUserBookingStatsQuery request, 
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
