using FluentValidation;

namespace EasyShare.Application.Features.Bookings.Commands.UpdateBooking;
public class UpdateBookingCommandValidator 
    : AbstractValidator<UpdateBookingCommand>
{
    public UpdateBookingCommandValidator()
    {
        RuleFor(x => x.BookingId)
            .GreaterThan(0)
            .WithMessage("Недійсний ID бронювання.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Кількість товарів має бути більше нуля.");

        RuleFor(x => x.StartDate)
            .GreaterThan(DateTimeOffset.UtcNow)
            .WithMessage("Дата початку має бути в майбутньому.");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .WithMessage("Дата завершення має бути після дати початку.");
    }
}
