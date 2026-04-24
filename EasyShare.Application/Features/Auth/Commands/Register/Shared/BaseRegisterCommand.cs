using MediatR;
using EasyShare.Application.Features.Auth.Queries.Login;

namespace EasyShare.Application.Features.Auth.Commands.Register.Shared;

public abstract record BaseRegisterCommand : IRequest<AuthResponseDto>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string PhoneNumber { get; init; }
}