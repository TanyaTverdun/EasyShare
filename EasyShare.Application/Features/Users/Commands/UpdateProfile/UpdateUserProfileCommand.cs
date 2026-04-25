using MediatR;

namespace EasyShare.Application.Features.Users.Commands.UpdateProfile;

public record UpdateUserProfileCommand : IRequest<UserProfileResponse>
{
    public required string FullName { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }

    public string? City { get; init; }
    public string? Street { get; init; }
    public int? Building { get; init; }
}
