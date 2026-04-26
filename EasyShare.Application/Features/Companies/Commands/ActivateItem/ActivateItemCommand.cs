using MediatR;

namespace EasyShare.Application.Features.Companies.Commands.ActivateItem;

public record ActivateItemCommand : IRequest<Unit>
{
    public int Id { get; init; }
}
