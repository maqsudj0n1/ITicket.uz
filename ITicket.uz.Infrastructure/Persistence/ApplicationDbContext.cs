using ITicket.uz.Application.Commons.Interfaces;
using ITicket.uz.Domain.Entities;
using ITicket.uz.Domain.Identity;
using ITicket.uz.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
namespace ITicket.uz.Infrastructure.Persistence;
public class ApplicationDbContext:DbContext, IApplicationDbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public ApplicationDbContext(
      DbContextOptions<ApplicationDbContext> options,
      AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
      : base(options)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;

    }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Venue> Venues { get; set;}
    public DbSet<User> Users { get; set;}
    public DbSet<Event> Events { get; set;}
    public DbSet<Customer> Customers { get; set;}
    public DbSet<Reservation> Reservations { get; set;}
    public DbSet<Row> Rows { get; set;}
    public DbSet<ScheduledEvent> ScheduledEvents { get; set;}
    public DbSet<Role> Roles { get; set;}
    public DbSet<Permission> Permissions { get; set;}
    public DbSet<UserRefreshToken> UserRefreshTokens { get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }
}