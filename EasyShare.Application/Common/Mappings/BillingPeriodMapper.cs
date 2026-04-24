using EasyShare.Domain.Enums;

namespace EasyShare.Application.Common.Mappings;

public static class BillingPeriodMapper
{
    public static string ToFriendlyString(this BillingPeriod period)
    {
        return period switch
        {
            BillingPeriod.Daily => "День",
            BillingPeriod.Monthly => "Місяць",

            _ => throw new ArgumentOutOfRangeException(nameof(period), $"Невідомий період тарифікації: {period}")
        };
    }
}
