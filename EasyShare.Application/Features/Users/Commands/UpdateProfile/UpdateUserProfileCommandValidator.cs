using FluentValidation;
namespace EasyShare.Application.Features.Users.Commands.UpdateProfile;

public class UpdateUserProfileCommandValidator 
    : AbstractValidator<UpdateUserProfileCommand>
{
    public UpdateUserProfileCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Ім'я та прізвище є обов'язковими")
            .MaximumLength(101)
            .WithMessage("ПІБ занадто довге (максимум {MaxLength} символів)");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email є обов'язковим")
            .EmailAddress()
            .WithMessage("Некоректний формат Email");

        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage("Номер телефону є обов'язковим")
            .Matches(@"^\+380\d{9}$")
            .WithMessage("Формат має бути +380XXXXXXXXX");

        RuleFor(x => x.City)
            .MaximumLength(50)
            .WithMessage("Назва міста занадто довга (максимум {MaxLength} символів)");

        RuleFor(x => x.Street)
            .MaximumLength(100)
            .WithMessage("Назва вулиці занадто довга (максимум {MaxLength} символів)");

        RuleFor(x => x.Building)
            .GreaterThan(0)
            .When(x => x.Building.HasValue)
            .WithMessage("Номер будинку має бути додатним числом");
    }
}
