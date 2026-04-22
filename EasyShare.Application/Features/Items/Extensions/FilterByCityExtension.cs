using EasyShare.Domain.Entities;

namespace EasyShare.Application.Features.Items.Extensions;

public static class FilterByCityExtension
{
    public static IQueryable<ItemCatalogView> FilterByCity(
    this IQueryable<ItemCatalogView> query,
    string? city)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            return query;
        }

        return query.Where(i =>
            i.City.ToLower() == city.ToLower());
    }
}
