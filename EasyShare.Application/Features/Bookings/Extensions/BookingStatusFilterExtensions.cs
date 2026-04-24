using EasyShare.Domain.Entities;
using EasyShare.Domain.Enums;

namespace EasyShare.Application.Features.Bookings.Extensions;

public static class BookingStatusFilterExtensions
{
    public static IQueryable<Booking> ApplyStatusFilter(
        this IQueryable<Booking> query, 
        BookingStatus? status)
    {
        if (!status.HasValue)
        {
            return query;
        }

        return query.Where(b => b.Status == status.Value);
    }
}
