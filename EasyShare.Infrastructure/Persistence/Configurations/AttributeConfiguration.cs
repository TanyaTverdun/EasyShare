using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Infrastructure.Persistence.Configurations;

public class AttributeConfiguration : IEntityTypeConfiguration<Domain.Entities.Attribute>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Attribute> builder)
    {
        builder
            .ToTable("attributes");

        builder
            .HasKey(a => a.Id);

        builder
            .Property(a => a.Id)
            .HasColumnName("attribute_id");

        builder
            .Property(a => a.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(a => a.IsDeleted)
            .HasColumnName("is_deleted")
            .HasDefaultValue(false)
            .IsRequired();

        builder
            .Property(a => a.TypeId)
            .HasColumnName("type_id");

        builder
            .HasOne(a => a.ItemType)
            .WithMany(it => it.Attributes)
            .HasForeignKey(a => a.TypeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
