using FluentValidation;

namespace EasyShare.Application.Features.Auth.Queries.Login;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email є обов'язковим")
            .EmailAddress()
            .WithMessage("Некоректний формат Email");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Пароль є обов'язковим");
    }
}