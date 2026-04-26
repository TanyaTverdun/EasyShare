using FluentValidation;

namespace EasyShare.Application.Features.Items.Commands.UpdateItem;

public class UpdateItemCommandValidator
: AbstractValidator<UpdateItemCommand>
{
    public UpdateItemCommandValidator()
    {
        RuleFor(x => x.Name)
        .NotEmpty()
        .WithMessage("Назва товару обов'язкова.")
        .MaximumLength(100)
        .WithMessage("Назва не може перевищувати 100 символів.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Опис товару обов'язковий.");

        RuleFor(x => x.StockQuantity)
            .GreaterThan(0)
            .WithMessage("Кількість товару має бути більшою за 0.");

        RuleFor(x => x.TypeId)
            .GreaterThan(0)
            .WithMessage("Необхідно обрати тип товару.");

        RuleFor(x => x.BillingPeriod)
            .IsInEnum()
            .WithMessage("Обрано некоректний період оренди.");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Ціна оренди має бути більшою за 0.");

        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("Місто обов'язкове.");

        RuleFor(x => x.Street)
            .NotEmpty()
            .WithMessage("Вулиця обов'язкова.");

        RuleFor(x => x.Building)
            .GreaterThan(0)
            .WithMessage("Номер будинку має бути більшим за 0.");

        RuleFor(x => x.MaxRentDays)
            .GreaterThan(0)
            .When(x => x.MaxRentDays.HasValue)
            .WithMessage(
                "Максимальна кількість днів оренди має бути більшою за 0.");

        RuleFor(x => x.DepositAmount)
            .GreaterThanOrEqualTo(0)
            .When(x => x.DepositAmount.HasValue)
            .WithMessage("Сума застави не може бути від'ємною.");

        RuleFor(x => x.PrepaymentPercent)
            .InclusiveBetween(0, 100)
            .When(x => x.PrepaymentPercent.HasValue)
            .WithMessage("Відсоток передоплати має бути від 0 до 100.");

        RuleForEach(x => x.Attributes)
            .SetValidator(new UpdateItemAttributeDtoValidator());
    }
}
