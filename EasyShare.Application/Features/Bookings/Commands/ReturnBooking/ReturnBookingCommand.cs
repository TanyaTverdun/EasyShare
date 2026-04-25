using MediatR;

namespace EasyShare.Application.Features.Bookings.Commands.ReturnBooking;

public record ReturnBookingCommand : IRequest<Unit>
{
    public required int BookingId { get; init; }
    public int? Rating { get; init; }
    public string? Comment { get; init; }
}
