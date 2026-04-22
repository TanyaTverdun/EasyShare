using EasyShare.Domain.Entities;

namespace EasyShare.Application.Features.Items.Extensions;

public static class FilterByHierarchyExtension
{
    public static IQueryable<ItemCatalogView> FilterByHierarchy(
    this IQueryable<ItemCatalogView> query,
    List<int>? categoryIds,
    List<int>? typeIds)
    {
        if (categoryIds != null && categoryIds.Any())
        {
            query = query
                .Where(i => categoryIds.Contains(i.CategoryId));
        }

        if (typeIds != null && typeIds.Any())
        {
            query = query
                .Where(i => typeIds.Contains(i.TypeId));
        }

        return query;
    }
}
