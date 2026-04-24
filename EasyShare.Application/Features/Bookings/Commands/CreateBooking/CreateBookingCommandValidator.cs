using FluentValidation;

namespace EasyShare.Application.Features.Bookings.Commands.CreateBooking;

public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
{
    public CreateBookingCommandValidator()
    {
        RuleFor(x => x.ItemId)
            .GreaterThan(0)
            .WithMessage("Некоректний ID товару.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Кількість має бути більшою за нуль.");

        RuleFor(x => x.StartDate)
            .NotEmpty()
            .WithMessage("Вкажіть дату початку оренди.")
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
            .WithMessage("Дата початку не може бути в минулому.");

        RuleFor(x => x.EndDate)
            .NotEmpty()
            .WithMessage("Вкажіть дату завершення оренди.")
            .GreaterThan(x => x.StartDate)
            .WithMessage("Дата завершення має бути пізнішою за дату початку.");
    }
}