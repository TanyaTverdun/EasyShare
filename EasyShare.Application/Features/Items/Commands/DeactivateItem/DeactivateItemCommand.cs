using MediatR;

namespace EasyShare.Application.Features.Items.Commands.DeactivateItem;

public record DeactivateItemCommand : IRequest<Unit>
{
    public int Id { get; init; }
}