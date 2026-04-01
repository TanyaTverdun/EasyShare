using EasyShare.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Infrastructure.Persistence.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder
            .ToTable("reviews");

        builder
            .HasKey(r => r.Id);

        builder
            .Property(r => r.Id)
            .HasColumnName("review_id");

        builder
            .Property(r => r.Rating)
            .HasColumnName("rating")
            .IsRequired();

        builder
            .Property(r => r.Comment)
            .HasColumnName("comment")
            .HasColumnType("text");

        builder
            .Property(r => r.IsOwner)
            .HasColumnName("is_owner")
            .IsRequired();

        builder
            .Property(r => r.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamptz")
            .IsRequired();

        builder
            .Property(r => r.BookingId)
            .HasColumnName("booking_id");

        builder
            .HasOne(r => r.Booking)
            .WithMany(b => b.Reviews)
            .HasForeignKey(r => r.BookingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
