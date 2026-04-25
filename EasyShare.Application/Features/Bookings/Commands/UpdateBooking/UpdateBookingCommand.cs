using MediatR;

namespace EasyShare.Application.Features.Bookings.Commands.UpdateBooking;

public class UpdateBookingCommand : IRequest<Unit>
{
    public int BookingId { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public int Quantity { get; set; }
}
