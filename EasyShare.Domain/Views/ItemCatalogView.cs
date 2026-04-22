namespace EasyShare.Domain.Entities;

public record ItemCatalogView
{
    public int ItemId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public string ImageUrl { get; init; }
    public int TypeId { get; set; }
    public string TypeName { get; init; }
    public string CompanyName { get; init; }
    public string City { get; set; }
    public string Street { get; set; }
    public int Building { get; set; }
    public double AverageRating { get; init; }
    public int ReviewsCount { get; init; }
}