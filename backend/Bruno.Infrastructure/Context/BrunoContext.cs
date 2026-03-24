using Bruno.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bruno.Infrastructure.Context;

public class BrunoContext : DbContext
{
	public DbSet<Vehicle> Vehicles { get; set; }
	public DbSet<Customer> Customers { get; set; }
	public DbSet<Booking> Bookings { get; set; }

	public BrunoContext(DbContextOptions<BrunoContext> contextOptions) : base(contextOptions) { }

	override protected void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Vehicle>().HasQueryFilter(v => !v.IsDeleted);

		modelBuilder.Entity<Booking>(entity =>
		{
			entity.OwnsOne(b => b.DateRange, dr =>
			{
				dr.Property(x => x.StartDate).HasColumnName("StartDate");
				dr.Property(x => x.EndDate).HasColumnName("EndDate");
			});
		});
	}
}
