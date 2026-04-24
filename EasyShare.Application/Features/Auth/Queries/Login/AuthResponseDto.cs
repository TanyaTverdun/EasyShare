using EasyShare.Domain.Enums;

namespace EasyShare.Application.Features.Auth.Queries.Login;

public record AuthResponseDto
{
    public required string Token { get; init; }
    public required string DisplayName { get; init; }
    public required AccountType Role { get; init; }
}