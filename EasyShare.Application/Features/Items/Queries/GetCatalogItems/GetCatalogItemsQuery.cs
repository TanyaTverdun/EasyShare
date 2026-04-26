using EasyShare.Application.Common.Models;
using MediatR;

namespace EasyShare.Application.Features.Items.Queries.GetCatalogItems;

public record GetCatalogItemsQuery 
    : IRequest<PagedResult<CatalogItemDto>>
{
    public string? SearchTerm { get; init; }
    public string? City { get; init; }

    public CatalogSortOption SortBy { get; init; } = CatalogSortOption.Newest;

    public DateTime? AvailableFrom { get; init; }
    public DateTime? AvailableTo { get; init; }
    public int? MinQuantity { get; init; }

    public List<int>? CategoryIds { get; init; }
    public List<int>? TypeIds { get; init; }

    public Dictionary<int, List<string>>? Attributes { get; init; }

    public string? UserCity { get; init; }
    public string? UserStreet { get; init; }
    public int? UserBuilding { get; init; }

    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}