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

        foreach (var attribute in attributes)
        {
            int attrId = attribute.Key;
            List<string> attrValues = attribute.Value;

            if (attrValues.Any())
            {
                query = query.Where(catalogItem => context.ItemAttributeValues
                    .Any(iav => iav.ItemId == catalogItem.ItemId
                             && iav.AttributeId == attrId
                             && attrValues.Contains(iav.Value)));
            }
        }

        return query;
    }
}
