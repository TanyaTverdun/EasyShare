using FluentValidation;

namespace EasyShare.Application.Features.Companies.Commands.CreateItem;

public class CreateItemAttributeDtoValidator 
    : AbstractValidator<CreateItemAttributeDto>
{
    public CreateItemAttributeDtoValidator()
    {
        RuleFor(x => x.AttributeId)
            .GreaterThan(0)
            .WithMessage("Некоректний ID характеристики.");

        RuleFor(x => x.Value)
            .NotEmpty()
            .WithMessage("Значення характеристики не може бути порожнім.")
            .MaximumLength(100)
            .WithMessage("Значення характеристики занадто довге.");
    }
}
