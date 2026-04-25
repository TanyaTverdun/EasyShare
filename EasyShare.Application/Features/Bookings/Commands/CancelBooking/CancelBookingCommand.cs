using MediatR;

namespace EasyShare.Application.Features.Bookings.Commands.CancelBooking;

public class CancelBookingCommand : IRequest<Unit>
{
    public int BookingId { get; set; }
}
