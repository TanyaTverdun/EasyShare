using MediatR;

namespace EasyShare.Application.Features.Bookings.Commands.ConfirmBooking;

public record ConfirmBookingCommand : IRequest<Unit>
{
    public int Id { get; init; }
}
