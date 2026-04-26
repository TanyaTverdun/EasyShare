using MediatR;

namespace EasyShare.Application.Features.Companies.Commands.DeactivateItem;

public record DeactivateItemCommand : IRequest<Unit>
{
    public int Id { get; init; }
}