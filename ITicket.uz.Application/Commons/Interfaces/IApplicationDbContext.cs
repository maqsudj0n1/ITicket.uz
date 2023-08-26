using ITicket.uz.Domain.Entities;
using ITicket.uz.Domain.Identity;
using Microsoft.EntityFrameworkCore;
namespace ITicket.uz.Application.Commons.Interfaces;
public interface IApplicationDbContext
{
    DbSet<Seat> Seats { get; }
    DbSet<Venue> Venues { get; }
    DbSet<User> Users { get; }
    DbSet<Event> Events { get; }
    DbSet<Customer> Customers { get; }
    DbSet<Reservation> Reservations { get; }
    DbSet<Row> Rows { get; }
    DbSet<ScheduledEvent> ScheduledEvents { get; }
    DbSet<Role> Roles { get; }
    DbSet<Permission> Permissions { get; }
    DbSet<UserRefreshToken> UserRefreshTokens { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
