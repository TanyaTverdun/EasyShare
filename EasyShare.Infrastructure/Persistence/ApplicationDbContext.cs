using EasyShare.Application.Common.Interfaces;
using EasyShare.Domain.Entities;
using EasyShare.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
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

        public DbSet<ItemCatalogView> ItemCatalog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public async Task ExecuteCreateBookingProcedureAsync(
            int userId, 
            int itemId, 
            int quantity, 
            DateTime startDate, 
            DateTime endDate, 
            CancellationToken cancellationToken)
        {
            await Database.ExecuteSqlInterpolatedAsync(
                $"CALL sp_create_booking_safe({userId}, {itemId}, {quantity}, {startDate}, {endDate})",
                cancellationToken);
        }

        public async Task ExecuteUpdateBookingProcedureAsync(
            int bookingId,
            DateTime startDate,
            DateTime endDate,
            int quantity,
            CancellationToken cancellationToken)
        {
            await Database.ExecuteSqlInterpolatedAsync(
                $"CALL sp_update_booking_safe({bookingId}, {startDate}, {endDate}, {quantity})",
                cancellationToken);
        }
    }
}
