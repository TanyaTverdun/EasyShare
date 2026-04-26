using MediatR;

namespace EasyShare.Application.Features.Locations.Queries;

public record GetMyCompanyLocationQuery : IRequest<LocationDto?>;
