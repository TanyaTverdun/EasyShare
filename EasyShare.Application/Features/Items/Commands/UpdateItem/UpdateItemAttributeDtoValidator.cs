using FluentValidation;

namespace EasyShare.Application.Features.Items.Commands.UpdateItem;

internal class UpdateItemAttributeDtoValidator
    : AbstractValidator<UpdateItemAttributeDto>
{
    public UpdateItemAttributeDtoValidator()
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
