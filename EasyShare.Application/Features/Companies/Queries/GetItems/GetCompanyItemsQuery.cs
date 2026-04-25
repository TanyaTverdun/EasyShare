using EasyShare.Application.Features.Companies.Enum;
using MediatR;

namespace EasyShare.Application.Features.Companies.Queries.GetItems;

public record GetCompanyItemsQuery : IRequest<List<CompanyItemDto>>
{
    public string? SearchTerm { get; init; }
    public bool? IsActive { get; init; }
    public ItemSortOption? SortBy { get; init; }
}
