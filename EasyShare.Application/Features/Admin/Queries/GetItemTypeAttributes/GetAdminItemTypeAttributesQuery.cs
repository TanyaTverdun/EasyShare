using MediatR;

namespace EasyShare.Application.Features.Admin.Queries.GetItemTypeAttributes;

public record GetAdminItemTypeAttributesQuery: IRequest<List<AdminAttributeDto>>
{
    public int TypeId { get; init; }
}
