using EasyShare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyShare.Infrastructure.Persistence.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder
                .ToTable("locations");

            builder
                .HasKey(l => l.Id);

            builder
                .Property(l => l.Id)
                .HasColumnName("location_id");

            builder
                .Property(l => l.City)
                .HasColumnName("city")
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(l => l.Street)
                .HasColumnName("street")
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(l => l.Building)
                .HasColumnName("building")
                .IsRequired();
        }
    }
}
