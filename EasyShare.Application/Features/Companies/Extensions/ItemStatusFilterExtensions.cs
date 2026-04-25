using EasyShare.Domain.Entities;

namespace EasyShare.Application.Features.Companies.Extensions;

public static class ItemStatusFilterExtensions
{
    public static IQueryable<Item> FilterByStatus(
        this IQueryable<Item> query, 
        bool? isActive)
    {
        if (!isActive.HasValue)
        {
            return query;
        }

        return query.Where(i => i.IsActive == isActive.Value);
    }
}
