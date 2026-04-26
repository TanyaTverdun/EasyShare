using FluentValidation;

namespace EasyShare.Application.Features.Admin.Commands.UpdateCategory;

public class UpdateCategoryCommandValidator 
    : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty()
            .WithMessage("Назва категорії не може бути порожньою.")
            .MaximumLength(100)
            .WithMessage("Назва не повинна перевищувати 100 символів.");
    }
}
