using EasyShare.Application.Features.Auth.Commands.Register.Shared;
using FluentValidation;

namespace EasyShare.Application.Features.Auth.Commands.Register.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        Include(new BaseRegisterCommandValidator<RegisterUserCommand>());

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("Ім'я є обов'язковим")
            .MaximumLength(50)
            .WithMessage("Ім'я занадто довге");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Прізвище є обов'язковим")
            .MaximumLength(50)
            .WithMessage("Прізвище занадто довге");
    }
}