using MediatR;

namespace EasyShare.Application.Features.Attributes.Commands.DeleteAttribute;

public record DeleteAttributeCommand : IRequest<Unit>
{
    public required int Id { get; init; }
}
