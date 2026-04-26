using EasyShare.Domain.Enums;

namespace EasyShare.Application.Features.Companies.Queries.GetBookings;

public record CompanyBookingDto
{
    public int Id { get; init; }

    public required string ItemName { get; init; }
    public string? ItemImageUrl { get; init; }

    public required string UserName { get; init; }

    public DateTimeOffset StartDate { get; init; }
    public DateTimeOffset EndDate { get; init; }
    public decimal TotalPrice { get; init; }
    public BookingStatus Status { get; init; }
}
