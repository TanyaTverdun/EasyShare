using MediatR;

namespace EasyShare.Application.Features.Items.Commands.ActivateItem;

public record ActivateItemCommand : IRequest<Unit>
{
    public int Id { get; init; }
}
