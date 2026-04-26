using MediatR;

namespace EasyShare.Application.Features.Admin.Commands.DeleteItemType;

public record DeleteItemTypeCommand : IRequest<Unit>
{
    public required int Id { get; init; }
}
