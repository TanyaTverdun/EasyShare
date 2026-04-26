using MediatR;

namespace EasyShare.Application.Features.Attributes.Commands.CreateAttribute;

public record CreateAttributeCommand : IRequest<int>
{
    public required string Name { get; init; }
    public int TypeId { get; init; }
}
