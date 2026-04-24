using EasyShare.Application.Features.Auth.Commands.Register.Shared;

namespace EasyShare.Application.Features.Auth.Commands.Register.RegisterCompany;

public record RegisterCompanyCommand : BaseRegisterCommand
{
    public required string CompanyName { get; init; }
}