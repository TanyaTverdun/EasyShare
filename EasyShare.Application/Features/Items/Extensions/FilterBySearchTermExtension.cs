using EasyShare.Domain.Entities;

namespace EasyShare.Application.Features.Items.Extensions;

public static class FilterBySearchTermExtension
{
    public static IQueryable<ItemCatalogView> FilterBySearchTerm(
    this IQueryable<ItemCatalogView> query,
    string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return query;
        }

        return query.Where(i =>
            i.Name.ToLower().Contains(searchTerm.ToLower()));
    }
}
