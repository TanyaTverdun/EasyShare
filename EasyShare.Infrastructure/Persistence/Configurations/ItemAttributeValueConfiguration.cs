using EasyShare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyShare.Infrastructure.Persistence.Configurations;

public class ItemAttributeValueConfiguration : IEntityTypeConfiguration<ItemAttributeValue>
{
    public void Configure(EntityTypeBuilder<ItemAttributeValue> builder)
    {
        builder
            .HasKey(it => new
            {
                it.ItemId,
                it.AttributeId
            });

        builder
            .Property(it => it.ItemId)
            .HasColumnName("item_id");

        builder
            .Property(it => it.AttributeId)
            .HasColumnName("attribute_id");

        builder
            .Property(it => it.Value)
            .HasColumnName("value")
            .HasMaxLength(255)
            .IsRequired();

        builder
            .HasOne(it => it.Item)
            .WithMany(i => i.ItemAttributeValues)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(it => it.Attribute)
            .WithMany(a => a.ItemAttributeValues)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
