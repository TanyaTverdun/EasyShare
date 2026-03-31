using EasyShare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyShare.Infrastructure.Persistence.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder
            .ToTable("items");

        builder
            .HasKey(i => i.Id);

        builder
            .Property(i => i.Id)
            .HasColumnName("item_id");

        builder
            .Property(i => i.Name)
            .HasColumnName("name")
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(i => i.Description)
            .HasColumnName("description")
            .HasColumnType("text")
            .IsRequired();

        builder
            .Property(i => i.ImageUrl)
            .HasColumnName("image_url")
            .HasMaxLength(255);

        builder
            .Property(i => i.StockQuantity)
            .HasColumnName("stock_quantity")
            .IsRequired();

        builder
            .Property(i => i.BillingPeriod)
            .HasColumnName("billing_period")
            .IsRequired();

        builder
            .Property(i => i.Price)
            .HasColumnName("price")
            .HasPrecision(10, 2)
            .IsRequired();

        builder
            .Property(i => i.DepositAmount)
            .HasColumnName("deposit_amount")
            .HasPrecision(10, 2);

        builder
            .Property(i => i.PrepaymentPercent)
            .HasColumnName("prepayment_percent");

        builder
            .Property(i => i.MaxRentDays)
            .HasColumnName("max_rent_days");

        builder
            .Property(i => i.IsActive)
            .HasColumnName("is_active")
            .IsRequired();

        builder
            .Property(i => i.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamptz")
            .IsRequired();

        builder
            .Property(i => i.CompanyId)
            .HasColumnName("company_id");

        builder
            .Property(i => i.TypeId)
            .HasColumnName("type_id");

        builder
            .Property(i => i.LocationId)
            .HasColumnName("location_id");

        builder
            .HasOne(i => i.Company)
            .WithMany(c => c.Items)
            .HasForeignKey(i => i.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(i => i.ItemType)
            .WithMany(t => t.Items)
            .HasForeignKey(i => i.TypeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(i => i.Location)
            .WithMany(l => l.Items)
            .HasForeignKey(i => i.LocationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
