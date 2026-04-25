using FluentValidation;

namespace EasyShare.Application.Features.Companies.Commands.UpdateProfile;

public class UpdateCompanyProfileCommandValidator 
    : AbstractValidator<UpdateCompanyProfileCommand>
{
    public UpdateCompanyProfileCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty()
            .WithMessage("Назва компанії є обов'язковою.")
            .MaximumLength(100)
            .WithMessage("Назва компанії не може перевищувати 100 символів.");

        RuleFor(v => v.Email)
            .NotEmpty()
            .WithMessage("Електронна пошта є обов'язковою.")
            .EmailAddress()
            .WithMessage("Невірний формат електронної пошти.")
            .MaximumLength(100)
            .WithMessage("Електронна пошта не може перевищувати 100 символів.");

        RuleFor(v => v.Phone)
            .NotEmpty()
            .WithMessage("Номер телефону є обов'язковим.")
            .Matches(@"^\+?[1-9]\d{7,14}$")
            .WithMessage("Формат має бути +380XXXXXXXXX");

        RuleFor(v => v.City)
            .MaximumLength(50)
            .WithMessage("Назва міста не може перевищувати 50 символів.")
            .When(v => !string.IsNullOrWhiteSpace(v.City));

        RuleFor(v => v.Street)
            .MaximumLength(100)
            .WithMessage("Назва вулиці не може перевищувати 100 символів.")
            .When(v => !string.IsNullOrWhiteSpace(v.Street));

        RuleFor(v => v.Building)
            .GreaterThan(0)
            .WithMessage("Номер будинку має бути більшим за 0.")
            .When(v => v.Building.HasValue);
    }
}
