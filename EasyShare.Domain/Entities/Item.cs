using EasyShare.Domain.Enums;

namespace EasyShare.Domain.Entities;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public int StockQuantity { get; set; }
    public BillingPeriod BillingPeriod { get; set; }
    public decimal Price { get; set; }
    public decimal? DepositAmount { get; set; }
    public int? PrepaymentPercent { get; set; }
    public int? MaxRentDays { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;

    public int TypeId { get; set; }
    public ItemType ItemType { get; set; } = null!;

    public int LocationId { get; set; }
    public Location Location { get; set; } = null!;

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<ItemAttributeValue> ItemAttributeValues { get; set; } = new List<ItemAttributeValue>();

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }
}
