using Bruno.Domain.Entities;
using Bruno.Domain.Enums;
using Bruno.Domain.Repositories;
using Bruno.Domain.ValueObjects;
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

	public async Task<IEnumerable<Booking>> ListFiltered(int skip, int take, BookingStatus? status, string? search)
	{
		var query = dbContext.Bookings
			.Include(booking => booking.Vehicle)
			.Include(booking => booking.Customer)
			.AsQueryable();

		if (status.HasValue)
			query = query.Where(b => b.Status == status.Value);

		if (!string.IsNullOrWhiteSpace(search))
		{
			var term = search.ToLower();
			query = query.Where(b =>
				b.Customer.FirstName.ToLower().Contains(term) ||
				b.Customer.LastName.ToLower().Contains(term) ||
				b.Vehicle.RegistrationNumber.ToLower().Contains(term) ||
				b.Vehicle.Make.ToLower().Contains(term) ||
				b.Vehicle.Model.ToLower().Contains(term));
		}

		return await query.Skip(skip).Take(take).ToListAsync();
	}

	public async Task<bool> HasOverlappingBookingAsync(Guid vehicleId, DateRange dateRange)
	{
		return await dbContext.Bookings.Where(b => b.VehicleId == vehicleId)
										.Where(b => b.Status != BookingStatus.Cancelled)
										.AnyAsync(b =>
											b.DateRange.StartDate < dateRange.EndDate &&
											b.DateRange.EndDate > dateRange.StartDate);
	}

	public async Task<bool> ExistsForCustomerAsync(Guid customerId)
	{
		return await dbContext.Bookings.AnyAsync(booking => booking.CustomerId == customerId);
	}
}
