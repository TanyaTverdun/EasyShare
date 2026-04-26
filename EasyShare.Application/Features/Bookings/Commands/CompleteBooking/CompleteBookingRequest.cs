namespace EasyShare.Application.Features.Bookings.Commands.CompleteBooking;

public record CompleteBookingRequest
{
    public int? Rating { get; init; }
    public string? Comment { get; init; }
}
