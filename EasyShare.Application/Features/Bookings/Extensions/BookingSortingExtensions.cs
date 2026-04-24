using EasyShare.Application.Features.Bookings.Enums;
using EasyShare.Domain.Entities;

namespace EasyShare.Application.Features.Bookings.Extensions;

public static class BookingSortingExtensions
{
    public static IQueryable<Booking> ApplySorting(
        this IQueryable<Booking> query, 
        BookingSortOption sortBy)
    {
        return sortBy switch
        {
            BookingSortOption.DateAsc => query
                .OrderBy(b => b.CreatedAt),
            BookingSortOption.PriceDesc => query
                .OrderByDescending(b => b.TotalPrice),
            BookingSortOption.PriceAsc => query
                .OrderBy(b => b.TotalPrice),
            _ => query.OrderByDescending(b => b.CreatedAt)
        };
    }
}
