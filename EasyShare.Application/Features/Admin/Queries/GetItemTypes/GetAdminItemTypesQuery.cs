using MediatR;

namespace EasyShare.Application.Features.Admin.Queries.GetItemTypes;

public record GetAdminItemTypesQuery : IRequest<List<AdminItemTypeDto>>
{
    public string? SearchTerm { get; init; }
}
