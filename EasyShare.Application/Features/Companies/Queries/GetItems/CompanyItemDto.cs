using EasyShare.Domain.Enums;

namespace EasyShare.Application.Features.Companies.Queries.GetItems;

public record CompanyItemDto
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public string Description { get; init; } = string.Empty;
    public string? ImageUrl { get; init; }
    public decimal Price { get; init; }
    public BillingPeriod BillingPeriod { get; init; }
    public bool IsActive { get; init; }

    public double Rating { get; init; }
    public int BookingsCount { get; init; }
}
