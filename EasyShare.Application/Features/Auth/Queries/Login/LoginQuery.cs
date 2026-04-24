using MediatR;

namespace EasyShare.Application.Features.Auth.Queries.Login;

public record LoginQuery : IRequest<AuthResponseDto>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}