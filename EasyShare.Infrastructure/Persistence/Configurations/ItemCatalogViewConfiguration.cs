using EasyShare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyShare.Infrastructure.Persistence.Configurations;

public class ItemCatalogViewConfiguration : IEntityTypeConfiguration<ItemCatalogView>
{
    public void Configure(EntityTypeBuilder<ItemCatalogView> builder)
    {
        builder
            .ToView("vw_item_catalog");

        builder
            .HasNoKey();

        builder
            .Property(v => v.ItemId)
            .HasColumnName("item_id");
        builder
            .Property(v => v.StockQuantity)
            .HasColumnName("stock_quantity");

        builder
            .Property(v => v.TypeId)
            .HasColumnName("type_id");

        builder
            .Property(v => v.CategoryId)
            .HasColumnName("category_id");

        builder
            .Property(v => v.Name)
            .HasColumnName("name");

        builder
            .Property(v => v.Description)
            .HasColumnName("description");

        builder
            .Property(v => v.Price)
            .HasColumnName("price");

        builder
            .Property(v => v.ImageUrl)
            .HasColumnName("image_url");

        builder
            .Property(v => v.TypeName)
            .HasColumnName("type_name");

        builder
            .Property(v => v.CompanyName)
            .HasColumnName("company_name");

        builder
            .Property(v => v.City)
            .HasColumnName("city");

        builder
            .Property(v => v.Street)
            .HasColumnName("street");

        builder
            .Property(v => v.Building)
            .HasColumnName("building");

        builder
            .Property(v => v.AverageRating)
            .HasColumnName("average_rating");

        builder
            .Property(v => v.ReviewsCount)
            .HasColumnName("reviews_count");
    }
}