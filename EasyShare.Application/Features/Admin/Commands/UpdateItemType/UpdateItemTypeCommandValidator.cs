using FluentValidation;

namespace EasyShare.Application.Features.Admin.Commands.UpdateItemType;

public class UpdateItemTypeCommandValidator 
    : AbstractValidator<UpdateItemTypeCommand>
{
    public UpdateItemTypeCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty()
            .WithMessage("Назва типу не може бути порожньою.")
            .MaximumLength(100)
            .WithMessage("Назва не повинна перевищувати 100 символів.");
    }
}
