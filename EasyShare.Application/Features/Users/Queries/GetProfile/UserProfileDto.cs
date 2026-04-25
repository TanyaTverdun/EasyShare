namespace EasyShare.Application.Features.Users.Queries.GetProfile;

public class UserProfileDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public string? City { get; set; }
    public string? Street { get; set; }
    public int? Building { get; set; }
}
