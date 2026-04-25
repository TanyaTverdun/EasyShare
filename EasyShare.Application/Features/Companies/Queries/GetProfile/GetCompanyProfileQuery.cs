using MediatR;

namespace EasyShare.Application.Features.Companies.Queries.GetProfile;

public record GetCompanyProfileQuery : IRequest<CompanyProfileDto>;
