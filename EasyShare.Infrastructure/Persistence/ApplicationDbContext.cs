using System;
using System.Collections.Generic;
using System.Linq;
using EasyShare.Domain.Entities;
using EasyShare.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Location> Locations => Set<Location>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Company> Companies => Set<Company>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<ItemType> ItemTypes => Set<ItemType>();
        public DbSet<Domain.Entities.Attribute> Attributes => Set<Domain.Entities.Attribute>();
        public DbSet<Item> Items => Set<Item>();
        public DbSet<ItemAttributeValue> ItemAttributeValues => Set<ItemAttributeValue>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<Review> Reviews => Set<Review>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasPostgresEnum<BookingStatus>("booking_status");

            modelBuilder
                .HasPostgresEnum<BillingPeriod>("billing_period");

            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
