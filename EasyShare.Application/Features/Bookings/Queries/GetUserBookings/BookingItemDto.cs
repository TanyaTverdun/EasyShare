using EasyShare.Domain.Enums;

namespace EasyShare.Application.Features.Bookings.Queries.GetUserBookings;

public class BookingItemDto
{
    public int Id { get; set; }
    public string ItemName { get; set; }
    public string ItemImageUrl { get; set; }
    public string CategoryName { get; set; }

    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public decimal TotalPrice { get; set; }

    public string Status { get; set; }

    public bool CanEdit => Status == BookingStatus.PendingConfirmation.ToString();
    public bool CanCancel => Status == BookingStatus.PendingConfirmation.ToString() ||
                             Status == BookingStatus.Confirmed.ToString();
}
