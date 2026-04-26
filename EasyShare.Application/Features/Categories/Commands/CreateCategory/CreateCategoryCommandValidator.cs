using FluentValidation;

namespace EasyShare.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator 
    : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Назва категорії обов'язкова.")
            .MaximumLength(100)
            .WithMessage("Назва занадто довга (макс 100 символів).");
    }
}
