using EasyShare.Application.Features.Auth.Commands.Register.Shared;
using FluentValidation;

namespace EasyShare.Application.Features.Auth.Commands.Register.RegisterCompany;

public class RegisterCompanyCommandValidator : AbstractValidator<RegisterCompanyCommand>
{
    public RegisterCompanyCommandValidator()
    {
        Include(new BaseRegisterCommandValidator<RegisterCompanyCommand>());

        RuleFor(x => x.CompanyName)
            .NotEmpty()
            .WithMessage("Назва компанії є обов'язковою")
            .MinimumLength(2)
            .WithMessage("Назва компанії занадто коротка");
    }
}