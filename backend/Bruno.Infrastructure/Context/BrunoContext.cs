using Bruno.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bruno.Infrastructure.Context;

public class BrunoContext : DbContext
{
	public DbSet<Vehicle> Vehicles { get; set; }
	public DbSet<Customer> Customers { get; set; }
	public DbSet<Booking> Bookings { get; set; }

	public BrunoContext(DbContextOptions<BrunoContext> contextOptions) : base(contextOptions) { }
}
