using MediatR;

namespace EasyShare.Application.Features.ItemTypes.Queries.GetTypesByCategory;

public record GetTypesByCategoryQuery : IRequest<List<CategoryWithTypesDto>>
{
    public required List<int> CategoryIds { get; init; }
}
