using EasyShare.Application.Features.Companies.Enum;
using EasyShare.Domain.Entities;

namespace EasyShare.Application.Features.Companies.Extensions;

public static class ItemSortingExtensions
{
    public static IQueryable<Item> Sort(
        this IQueryable<Item> query, 
        ItemSortOption? sortBy)
    {
        return sortBy switch
        {
            ItemSortOption.DateAsc => 
                query.OrderBy(i => i.CreatedAt),

            ItemSortOption.PriceAsc => 
                query.OrderBy(i => i.Price),

            ItemSortOption.PriceDesc => 
                query.OrderByDescending(i => i.Price),

            ItemSortOption.DateDesc or null => 
                query.OrderByDescending(i => i.CreatedAt),

            _ => query.OrderByDescending(i => i.CreatedAt)
        };
    }
}
