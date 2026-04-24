namespace EasyShare.Application.Features.Items.Queries.GetItemById;

public record ItemDetailsDto
{
    public required int ItemId { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string ImageUrl { get; init; }
    public required decimal Price { get; init; }
    public required string BillingPeriod { get; init; }
    public decimal? DepositAmount { get; init; }
    public int? PrepaymentPercent { get; init; }
    public int? MaxRentDays { get; init; }
    public required string City { get; init; }
    public required string Street { get; init; }
    public required int Building { get; init; }
    public required string CompanyName { get; init; }
    public required string CategoryName { get; init; }
    public required double AverageRating { get; init; }
    public required int ReviewsCount { get; init; }
    public required Dictionary<string, string> Attributes { get; init; }
    public required List<ItemReviewDto> Reviews { get; init; }
}
