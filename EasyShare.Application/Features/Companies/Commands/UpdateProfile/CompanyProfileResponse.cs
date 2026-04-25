namespace EasyShare.Application.Features.Companies.Commands.UpdateProfile;

public record CompanyProfileResponse
{
    public required string Token { get; init; }
    public required string NewEmail { get; init; }
    public required string NewName { get; init; }
}
