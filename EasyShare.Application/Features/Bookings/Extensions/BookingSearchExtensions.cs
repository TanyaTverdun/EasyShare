using EasyShare.Domain.Entities;

namespace EasyShare.Application.Features.Bookings.Extensions;

public static class BookingSearchExtensions
{
    public static IQueryable<Booking> ApplySearch(
        this IQueryable<Booking> query, 
        string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return query;
        }

        var term = searchTerm.ToLower();
        return query.Where(b => b.Item.Name.ToLower().Contains(term));
    }
}
