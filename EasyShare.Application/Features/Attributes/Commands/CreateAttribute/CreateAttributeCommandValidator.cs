using FluentValidation;

namespace EasyShare.Application.Features.Attributes.Commands.CreateAttribute;

public class CreateAttributeCommandValidator 
    : AbstractValidator<CreateAttributeCommand>
{
    public CreateAttributeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Назва характеристики обов'язкова.")
            .MaximumLength(100)
            .WithMessage("Назва занадто довга.");

        RuleFor(x => x.TypeId)
            .GreaterThan(0)
            .WithMessage("Невірний тип товару.");
    }
}
