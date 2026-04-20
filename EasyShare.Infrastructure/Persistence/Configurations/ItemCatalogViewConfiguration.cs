using EasyShare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyShare.Infrastructure.Persistence.Configurations;

public class ItemCatalogViewConfiguration : IEntityTypeConfiguration<ItemCatalogView>
{
    public void Configure(EntityTypeBuilder<ItemCatalogView> builder)
    {
        builder.ToView("vw_item_catalog");

        builder.HasNoKey();
    }
}