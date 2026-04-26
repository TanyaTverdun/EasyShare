using FluentValidation;

namespace EasyShare.Application.Features.ItemTypes.Commands.CreateItemType;

public class CreateItemTypeCommandValidator 
    : AbstractValidator<CreateItemTypeCommand>
{
    public CreateItemTypeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Назва типу обов'язкова.")
            .MaximumLength(100)
            .WithMessage("Назва занадто довга.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0)
            .WithMessage("Невірна категорія.");
    }
}
