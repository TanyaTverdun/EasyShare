using EasyShare.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Infrastructure.Persistence.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder
            .ToTable("companies");

        builder
            .HasKey(c => c.Id);

        builder
            .Property(c => c.Id)
            .HasColumnName("company_id");

        builder
            .Property(c => c.Name)
            .HasColumnName("name")
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(c => c.Email)
            .HasColumnName("email")
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(c => c.Phone)
            .HasColumnName("phone")
            .HasMaxLength(20)
            .IsRequired();

        builder
            .Property(c => c.PasswordHash)
            .HasColumnName("password_hash")
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(c => c.LocationId)
            .HasColumnName("location_id")
            .IsRequired(false);

        builder.HasOne(c => c.Location)
               .WithMany(l => l.Companies)
               .HasForeignKey(c => c.LocationId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
