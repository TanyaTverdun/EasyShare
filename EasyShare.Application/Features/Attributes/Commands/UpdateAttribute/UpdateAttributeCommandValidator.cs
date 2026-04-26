using FluentValidation;

namespace EasyShare.Application.Features.Attributes.Commands.UpdateAttribute;

public class UpdateAttributeCommandValidator 
    : AbstractValidator<UpdateAttributeCommand>
{
    public UpdateAttributeCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty()
            .WithMessage("Назва характеристики не може бути порожньою.")
            .MaximumLength(100)
            .WithMessage("Назва не повинна перевищувати 100 символів.");
    }
}
