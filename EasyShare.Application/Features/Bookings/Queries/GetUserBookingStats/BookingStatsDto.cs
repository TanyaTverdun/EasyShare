namespace EasyShare.Application.Features.Bookings.Queries.GetUserBookingStats;

public class BookingStatsDto
{
    public int PendingConfirmationCount { get; set; }
    public int ConfirmedCount { get; set; }
    public int ActiveCount { get; set; }
    public int PendingReturnCount { get; set; }
    public int CompletedCount { get; set; }
}
