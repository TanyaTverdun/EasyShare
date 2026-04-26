using MediatR;

namespace EasyShare.Application.Features.Bookings.Commands.IssueBooking;

public record IssueBookingCommand : IRequest
{
    public int Id { get; init; }
}
