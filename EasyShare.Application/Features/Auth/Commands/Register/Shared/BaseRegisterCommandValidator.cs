using FluentValidation;

namespace EasyShare.Application.Features.Auth.Commands.Register.Shared;

public class BaseRegisterCommandValidator<T> : AbstractValidator<T> 
    where T : BaseRegisterCommand
{
    public BaseRegisterCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email є обов'язковим")
            .EmailAddress()
            .WithMessage("Некоректний формат Email");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Пароль є обов'язковим")
            .MinimumLength(6)
            .WithMessage("Пароль має бути не менше 6 символів");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("Номер телефону є обов'язковим")
            .Matches(@"^\+380\d{9}$")
            .WithMessage("Формат має бути +380XXXXXXXXX");
    }
}