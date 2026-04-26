using EasyShare.Domain.Enums;
using MediatR;

namespace EasyShare.Application.Features.Companies.Commands.CreateItem;

public record CreateItemCommand : IRequest<int>
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public int StockQuantity { get; init; }

    public Stream? ImageStream { get; init; }
    public string? ImageFileName { get; init; }

    public int TypeId { get; init; }
    public BillingPeriod BillingPeriod { get; init; }
    public decimal Price { get; init; }

    public required string City { get; init; }
    public required string Street { get; init; }
    public int Building { get; init; }

    public int? MaxRentDays { get; init; }
    public decimal? DepositAmount { get; init; }
    public int? PrepaymentPercent { get; init; }

    public List<CreateItemAttributeDto> Attributes { get; init; } = new();
}
