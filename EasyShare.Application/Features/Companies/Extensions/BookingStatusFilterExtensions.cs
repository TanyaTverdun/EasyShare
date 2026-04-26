using EasyShare.Domain.Entities;
using EasyShare.Domain.Enums;

namespace EasyShare.Application.Features.Companies.Extensions;

public static class BookingStatusFilterExtensions
{
    public static IQueryable<Booking> FilterByStatus(
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
