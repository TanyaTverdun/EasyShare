using MediatR;

namespace EasyShare.Application.Features.Attributes.Commands.UpdateAttribute;

public record UpdateAttributeCommand : IRequest<Unit>
{
    public int Id { get; init; }
    public string Name { get; init; }
}
