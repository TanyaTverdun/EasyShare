using MediatR;

namespace EasyShare.Application.Features.Admin.Commands.UpdateItemType;

public record UpdateItemTypeCommand : IRequest<Unit>
{
    public int Id { get; init; }
    public string Name { get; init; }
}
