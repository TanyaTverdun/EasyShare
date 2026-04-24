using EasyShare.Application.Features.Auth.Commands.Register.Shared;

namespace EasyShare.Application.Features.Auth.Commands.Register.RegisterUser;

public record RegisterUserCommand : BaseRegisterCommand
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
}