using MediatR;

namespace EasyShare.Application.Features.ItemTypes.Commands.CreateItemType;

public record CreateItemTypeCommand : IRequest<int>
{
    public required string Name { get; init; }
    public int CategoryId { get; init; }
}
