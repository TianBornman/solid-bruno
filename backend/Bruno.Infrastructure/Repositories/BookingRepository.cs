using Bruno.Domain.Entities;
using Bruno.Domain.Repositories;
using Bruno.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Bruno.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
	private readonly BrunoContext dbContext;

	public BookingRepository(BrunoContext dbContext)
	{
		this.dbContext = dbContext;
	}

	public async Task Add(Booking entity)
	{
		await dbContext.Bookings.AddAsync(entity);
		await dbContext.SaveChangesAsync();
	}

	public async Task Update(Booking entity)
	{
		dbContext.Update(entity);
		await dbContext.SaveChangesAsync();
	}

	public async Task Delete(Booking entity)
	{
		dbContext.Bookings.Remove(entity);
		await dbContext.SaveChangesAsync();
	}

	public async Task<Booking?> Get(Guid id)
	{
		return await dbContext.Bookings.Include(booking => booking.Vehicle)
									   .Include(booking => booking.Customer)
									   .FirstOrDefaultAsync(booking => booking.Id == id);
	}

	public async Task<IEnumerable<Booking>> List(int skip, int take)
	{
		return await dbContext.Bookings.Include(booking => booking.Vehicle)
									   .Include(booking => booking.Customer)
									   .Skip(skip).Take(take).ToListAsync();
	}
}
