using EasyShare.Domain.Entities;

namespace EasyShare.Application.Features.Companies.Extensions;

public static class BookingSearchExtensions
{
    public static IQueryable<Booking> Search(
        this IQueryable<Booking> query, 
        string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return query;
        }

        var search = searchTerm.ToLower();

        return query.Where(b =>
            b.Item.Name.ToLower().Contains(search) ||
            b.User.FirstName.ToLower().Contains(search));
    }
}
