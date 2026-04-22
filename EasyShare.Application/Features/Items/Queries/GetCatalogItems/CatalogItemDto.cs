namespace EasyShare.Application.Features.Items.Queries.GetCatalogItems;

public record CatalogItemDto
{
    public required int ItemId { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required decimal Price { get; init; }
    public required string ImageUrl { get; init; }
    public required string TypeName { get; init; }
    public required string CompanyName { get; init; }

    public required string City { get; init; }
    public required string Street { get; init; }
    public required int Building { get; init; }

    public required double AverageRating { get; init; }
    public required int ReviewsCount { get; init; }
}
