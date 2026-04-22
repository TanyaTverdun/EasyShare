using EasyShare.Application.Common.Interfaces;
using EasyShare.Domain.Entities;
using EasyShare.Domain.Enums;

namespace EasyShare.Application.Features.Items.Extensions;

public static class FilterByAvailabilityExtension
{
    public static IQueryable<ItemCatalogView> FilterByAvailability(
    this IQueryable<ItemCatalogView> query,
    int? minQuantity,
    DateTime? availableFrom,
    DateTime? availableTo,
    IApplicationDbContext context)
    {
        if (!minQuantity.HasValue)
        {
            return query;
        }

        if (availableFrom.HasValue && availableTo.HasValue)
        {
            var fromUtc = availableFrom.Value.ToUniversalTime();
            var toUtc = availableTo.Value.ToUniversalTime();

            return query.Where(catalogItem =>
                catalogItem.StockQuantity -
                context.Bookings
                    .Where(b => b.ItemId == catalogItem.ItemId
                             && b.Status != BookingStatus.Completed
                             && b.StartDatetime < toUtc
                             && b.EndDatetime > fromUtc)
                    .Sum(b => (int)b.RentedQuantity)
                >= minQuantity.Value);
        }

        return query.Where(i => i.StockQuantity >= minQuantity.Value);
    }
}
