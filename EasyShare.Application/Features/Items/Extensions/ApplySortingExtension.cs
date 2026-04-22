using EasyShare.Application.Features.Items.Queries.GetCatalogItems;
using EasyShare.Domain.Entities;

namespace EasyShare.Application.Features.Items.Extensions;

public static class ApplySortingExtension
{
    public static IQueryable<ItemCatalogView> ApplySorting(
    this IQueryable<ItemCatalogView> query,
    CatalogSortOption sortBy,
    string? userCity,
    string? userStreet,
    int? userBuilding)
    {
        return sortBy switch
        {
            CatalogSortOption.PriceAscending =>
                query.OrderBy(i => i.Price),
            CatalogSortOption.PriceDescending =>
                query.OrderByDescending(i => i.Price),
            CatalogSortOption.HighestRating =>
                query.OrderByDescending(i => i.AverageRating),

            CatalogSortOption.Nearest => query.OrderByDescending(i =>
                (!string.IsNullOrWhiteSpace(userCity) &&
                    i.City.ToLower() == userCity.ToLower())
                ? (
                    100 +
                    ((!string.IsNullOrWhiteSpace(userStreet) &&
                        i.Street.ToLower() == userStreet.ToLower())
                    ? (10 + ((userBuilding.HasValue &&
                        i.Building == userBuilding.Value) ? 1 : 0))
                    : 0)
                ) : 0
            ),

            CatalogSortOption.Newest or _ =>
                query.OrderByDescending(i => i.ItemId)
        };
    }
}
