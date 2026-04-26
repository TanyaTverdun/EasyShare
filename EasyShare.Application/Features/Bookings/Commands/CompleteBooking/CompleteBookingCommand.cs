using MediatR;

namespace EasyShare.Application.Features.Bookings.Commands.CompleteBooking;

public record CompleteBookingCommand : IRequest
{
    public int Id { get; init; }
}
