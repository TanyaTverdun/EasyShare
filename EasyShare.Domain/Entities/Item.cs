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

    public static Item Create(
        string name,
        string description,
        int companyId,
        int typeId,
        Location location,
        BillingPeriod billingPeriod,
        decimal price,
        int stockQuantity,
        int? maxRentDays,
        decimal? depositAmount,
        int? prepaymentPercent,
        string? imageUrl,
        Dictionary<int, string>? attributes = null)
    {
        var item = new Item
        {
            Name = name,
            Description = description,
            CompanyId = companyId,
            TypeId = typeId,
            Location = location,
            BillingPeriod = billingPeriod,
            Price = price,
            StockQuantity = stockQuantity,
            MaxRentDays = maxRentDays,
            DepositAmount = depositAmount,
            PrepaymentPercent = prepaymentPercent,
            ImageUrl = imageUrl,
            IsActive = true,
            CreatedAt = DateTimeOffset.UtcNow
        };

        if (attributes != null && attributes.Any())
        {
            foreach (var attr in attributes)
            {
                item.ItemAttributeValues.Add(new ItemAttributeValue
                {
                    AttributeId = attr.Key,
                    Value = attr.Value
                });
            }
        }

        return item;
    }

    public void UpdateDetails(
        string name,
        string description,
        int typeId,
        Location location,
        BillingPeriod billingPeriod,
        decimal price,
        int stockQuantity,
        int? maxRentDays,
        decimal? depositAmount,
        int? prepaymentPercent,
        string? newImageUrl,
        Dictionary<int, string>? attributes)
    {
        Name = name;
        Description = description;
        TypeId = typeId;
        Location = location;
        BillingPeriod = billingPeriod;
        Price = price;
        StockQuantity = stockQuantity;
        MaxRentDays = maxRentDays;
        DepositAmount = depositAmount;
        PrepaymentPercent = prepaymentPercent;

        if (!string.IsNullOrEmpty(newImageUrl))
        {
            ImageUrl = newImageUrl;
        }

        ItemAttributeValues.Clear();

        if (attributes != null && attributes.Any())
        {
            foreach (var attr in attributes)
            {
                ItemAttributeValues.Add(new ItemAttributeValue
                {
                    AttributeId = attr.Key,
                    Value = attr.Value
                });
            }
        }
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }
}
