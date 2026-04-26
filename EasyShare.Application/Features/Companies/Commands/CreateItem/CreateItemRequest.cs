using EasyShare.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace EasyShare.Application.Features.Companies.Commands.CreateItem;

public record CreateItemRequest
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public int StockQuantity { get; init; }

    public IFormFile? Image { get; init; }

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

    public CreateItemCommand ToCommand()
    {
        return new CreateItemCommand
        {
            Name = this.Name,
            Description = this.Description,
            StockQuantity = this.StockQuantity,

            ImageStream = this.Image?.OpenReadStream(),
            ImageFileName = this.Image?.FileName,

            TypeId = this.TypeId,
            BillingPeriod = this.BillingPeriod,
            Price = this.Price,
            City = this.City,
            Street = this.Street,
            Building = this.Building,
            MaxRentDays = this.MaxRentDays,
            DepositAmount = this.DepositAmount,
            PrepaymentPercent = this.PrepaymentPercent,
            Attributes = this.Attributes
        };
    }
}
