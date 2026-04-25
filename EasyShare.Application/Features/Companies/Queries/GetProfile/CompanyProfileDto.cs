namespace EasyShare.Application.Features.Companies.Queries.GetProfile;

public record CompanyProfileDto
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }

    public string? City { get; init; }
    public string? Street { get; init; }
    public int? Building { get; init; }
}
