using FluentValidation;

namespace EasyShare.Application.Features.Bookings.Commands.ReturnBooking;

public class ReturnBookingCommandValidator 
    : AbstractValidator<ReturnBookingCommand>
{
    public ReturnBookingCommandValidator()
    {
        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5)
            .When(x => x.Rating.HasValue)
            .WithMessage("Оцінка має бути від 1 до 5 зірочок.");

        RuleFor(x => x.Comment)
            .MaximumLength(1000)
            .When(x => !string.IsNullOrEmpty(x.Comment))
            .WithMessage("Коментар занадто довгий (максимум 1000 символів).");
    }
}
