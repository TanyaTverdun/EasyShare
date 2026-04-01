using EasyShare.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Location> Locations { get; }
    DbSet<User> Users { get; }
    DbSet<Company> Companies { get; }
    DbSet<Category> Categories { get; }
    DbSet<ItemType> ItemTypes { get; }
    DbSet<Domain.Entities.Attribute> Attributes { get; }
    DbSet<Item> Items { get; }
    DbSet<ItemAttributeValue> ItemAttributeValues { get; }
    DbSet<Booking> Bookings { get; }
    DbSet<Review> Reviews { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
