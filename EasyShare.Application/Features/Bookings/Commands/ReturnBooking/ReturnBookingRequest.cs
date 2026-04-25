namespace EasyShare.Application.Features.Bookings.Commands.ReturnBooking;

public record ReturnBookingRequest
{
    public int? Rating { get; init; }
    public string? Comment { get; init; }
}
