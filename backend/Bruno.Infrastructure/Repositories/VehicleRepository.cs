using Bruno.Domain.Entities;
using Bruno.Domain.Repositories;
using Bruno.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Bruno.Infrastructure.Repositories;

public class VehicleRepository : IVehicleRepository
{
	private readonly BrunoContext dbContext;

	public VehicleRepository(BrunoContext dbContext)
	{
		this.dbContext = dbContext;
	}

	public async Task Add(Vehicle entity)
	{
		await dbContext.Vehicles.AddAsync(entity);
		await dbContext.SaveChangesAsync();
	}

	public async Task Update(Vehicle entity)
	{
		dbContext.Update(entity);
		await dbContext.SaveChangesAsync();
	}

	public async Task Delete(Vehicle entity)
	{
		dbContext.Vehicles.Remove(entity);
		await dbContext.SaveChangesAsync();
	}

	public async Task<Vehicle?> Get(Guid id)
	{
		return await dbContext.Vehicles.SingleAsync(vehicle => vehicle.Id == id);
	}

	public async Task<IEnumerable<Vehicle>> List(int skip, int take)
	{
		return await dbContext.Vehicles.Skip(skip).Take(0).ToListAsync();
	}
}
