using MediatR;

namespace EasyShare.Application.Features.Bookings.Commands.CreateBooking;

public record CreateBookingCommand : IRequest<Unit>
{
    public int ItemId { get; init; }
    public int Quantity { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
}
