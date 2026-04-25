using MediatR;

namespace EasyShare.Application.Features.Companies.Commands.UpdateProfile;

public record UpdateCompanyProfileCommand 
    : IRequest<CompanyProfileResponse>
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }

    public string? City { get; init; }
    public string? Street { get; init; }
    public int? Building { get; init; }
}
