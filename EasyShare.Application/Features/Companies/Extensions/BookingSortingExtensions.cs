using EasyShare.Domain.Entities;

namespace EasyShare.Application.Features.Companies.Extensions;

public static class BookingSortingExtensions
{
    public static IQueryable<Booking> SortByNewest(
        this IQueryable<Booking> query)
    {
        return query.OrderByDescending(b => b.CreatedAt);
    }
}
