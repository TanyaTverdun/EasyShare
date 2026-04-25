namespace EasyShare.Application.Features.Users.Commands.UpdateProfile;

public record UserProfileResponse
{
    public required string Token { get; init; }
    public required string NewEmail { get; init; }
    public required string NewName { get; init; }
}
