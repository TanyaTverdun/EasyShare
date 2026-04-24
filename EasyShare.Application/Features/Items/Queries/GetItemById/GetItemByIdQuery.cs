using MediatR;

namespace EasyShare.Application.Features.Items.Queries.GetItemById;

public record GetItemByIdQuery : IRequest<ItemDetailsDto>
{
    public required int Id { get; init; }
}
