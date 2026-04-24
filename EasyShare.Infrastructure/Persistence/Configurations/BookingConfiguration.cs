using EasyShare.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Infrastructure.Persistence.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.
            HasIndex(i => new
            {
                i.ItemId,
                i.StartDatetime,
                i.EndDatetime
            });

        builder.
            HasIndex(i => new
            {
                i.UserId,
                i.CreatedAt
            });

        builder
            .ToTable("bookings");

        builder
            .HasKey(b => b.Id);

        builder
            .Property(b => b.Id)
            .HasColumnName("booking_id");

        builder
            .Property(b => b.RentedQuantity)
            .HasColumnName("rented_quantity")
            .HasColumnType("smallint")
            .IsRequired();

        builder
            .Property(b => b.StartDatetime)
            .HasColumnName("start_datetime")
            .HasColumnType("timestamptz")
            .IsRequired();

        builder
            .Property(b => b.EndDatetime)
            .HasColumnName("end_datetime")
            .HasColumnType("timestamptz")
            .IsRequired();

        builder
            .Property(b => b.TotalPrice)
            .HasColumnName("total_price")
            .HasPrecision(10, 2)
            .IsRequired();

        builder
            .Property(b => b.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .IsRequired();

        builder
            .Property(b => b.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamptz")
            .IsRequired();

        builder
            .Property(b => b.ItemId)
            .HasColumnName("item_id");

        builder
            .Property(b => b.UserId)
            .HasColumnName("user_id");

        builder
            .HasOne(b => b.Item)
            .WithMany(i => i.Bookings)
            .HasForeignKey(b => b.ItemId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(b => b.User)
            .WithMany(u => u.Bookings)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
