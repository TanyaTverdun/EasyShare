using MediatR;

namespace EasyShare.Application.Features.Attributes.Queries.GetAttributesByType;

public record GetAttributesByTypeQuery : IRequest<List<TypeWithAttributesDto>>
{
    public required List<int> TypeIds { get; init; }
}
