using EasyShare.Application.Features.Bookings.Queries.GetUserBookingStats;
using EasyShare.Domain.Entities;
using EasyShare.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Features.Bookings.Extensions
{
    public static class BookingQueryExtensions
    {
        public static async Task<BookingStatsDto> GetStatsAsync(
            this IQueryable<Booking> query, 
            CancellationToken cancellationToken)
        {
            var stats = await query
                .GroupBy(b => 1)
                .Select(g => new BookingStatsDto
                {
                    PendingConfirmationCount = g
                        .Count(b => b.Status == BookingStatus.PendingConfirmation),
                    ConfirmedCount = g
                        .Count(b => b.Status == BookingStatus.Confirmed),
                    ActiveCount = g
                        .Count(b => b.Status == BookingStatus.Active),
                    PendingReturnCount = g
                        .Count(b => b.Status == BookingStatus.PendingReturn),
                    CompletedCount = g
                        .Count(b => b.Status == BookingStatus.Completed)
                })
                .FirstOrDefaultAsync(cancellationToken);

            return stats ?? new BookingStatsDto();
        }
    }
}
