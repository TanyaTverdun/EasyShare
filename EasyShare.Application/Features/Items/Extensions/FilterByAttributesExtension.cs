using EasyShare.Application.Common.Interfaces;
using EasyShare.Domain.Entities;

namespace EasyShare.Application.Features.Items.Extensions;

public static class FilterByAttributesExtension
{
    public static IQueryable<ItemCatalogView> FilterByAttributes(
    this IQueryable<ItemCatalogView> query,
    Dictionary<int, List<string>>? attributes,
    IApplicationDbContext context)
    {
        if (attributes == null || !attributes.Any())
        {
            return query;
        }

        var attrIds = attributes.Keys.ToList();
        var attrToType = context.Attributes
            .Where(a => attrIds.Contains(a.Id))
            .Select(a => new { a.Id, a.TypeId })
            .ToList()
            .ToDictionary(a => a.Id, a => a.TypeId);

        var attrsByType = attributes
            .GroupBy(kvp => attrToType.ContainsKey(kvp.Key) ? attrToType[kvp.Key] : -1)
            .ToList();

        foreach (var typeGroup in attrsByType)
        {
            var typeId = typeGroup.Key;

            foreach (var attr in typeGroup)
            {
                var attrId = attr.Key;
                var attrValues = attr.Value;

                query = query.Where(catalogItem =>
                    catalogItem.TypeId != typeId ||
                    context.ItemAttributeValues.Any(iav =>
                        iav.ItemId == catalogItem.ItemId &&
                        iav.AttributeId == attrId &&
                        attrValues.Contains(iav.Value)));
            }
        }

        return query;
    }
}
