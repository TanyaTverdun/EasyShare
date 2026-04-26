using EasyShare.Application.Common.Models;
using EasyShare.Application.Features.Companies.Enum;
using MediatR;

namespace EasyShare.Application.Features.Companies.Queries.GetItems;

public record GetCompanyItemsQuery 
    : IRequest<PagedResult<CompanyItemDto>>
{
    public string? SearchTerm { get; init; }
    public bool? IsActive { get; init; }
    public ItemSortOption? SortBy { get; init; }

    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
