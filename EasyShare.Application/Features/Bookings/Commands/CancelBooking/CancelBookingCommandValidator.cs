using FluentValidation;

namespace EasyShare.Application.Features.Bookings.Commands.CancelBooking;

public class CancelBookingCommandValidator 
    : AbstractValidator<CancelBookingCommand>
{
    public CancelBookingCommandValidator()
    {
        RuleFor(x => x.BookingId)
            .GreaterThan(0)
            .WithMessage("Недійсний ідентифікатор бронювання.");
    }
}
