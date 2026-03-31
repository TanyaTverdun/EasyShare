using EasyShare.Application.Common.Interfaces.Services;

namespace EasyShare.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
