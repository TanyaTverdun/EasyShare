using EasyShare.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Infrastructure.Persistence.Configurations;

public class ItemTypeConfiguration : IEntityTypeConfiguration<ItemType>
{
    public void Configure(EntityTypeBuilder<ItemType> builder)
    {
        builder
            .ToTable("item_types");

        builder
            .HasKey(it => it.Id);

        builder
            .Property(it => it.Id)
            .HasColumnName("type_id");

        builder
            .Property(it => it.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(it => it.IsDeleted)
            .HasColumnName("is_deleted")
            .HasDefaultValue(false)
            .IsRequired();

        builder
            .Property(it => it.CategoryId)
            .HasColumnName("category_id");

        builder.HasOne(it => it.Category)
               .WithMany(c => c.ItemTypes)
               .HasForeignKey(it => it.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
