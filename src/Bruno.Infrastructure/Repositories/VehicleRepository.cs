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
	}

	public Task Update(Vehicle entity)
	{
		dbContext.Update(entity);
		return Task.CompletedTask;
	}

	public Task Delete(Vehicle entity)
	{
		throw new NotSupportedException("Vehicles cannot deleted. Soft Delete instead.");
	}

	public Task SoftDelete(Vehicle entity)
	{
		entity.SoftDelete();
		dbContext.Update(entity);
		return Task.CompletedTask;
	}

	public async Task<Vehicle?> Get(Guid id)
	{
		return await dbContext.Vehicles.FirstOrDefaultAsync(vehicle => vehicle.Id == id);
	}

	public async Task<IEnumerable<Vehicle>> List(int skip, int take)
	{
		return await dbContext.Vehicles.Skip(skip).Take(take).ToListAsync();
	}

	public async Task<IEnumerable<Vehicle>> ListFiltered(int skip, int take, string? search)
	{
		var query = dbContext.Vehicles.AsQueryable();

		if (!string.IsNullOrWhiteSpace(search))
		{
			var term = search.ToLower();
			query = query.Where(v =>
				v.RegistrationNumber.ToLower().Contains(term) ||
				v.Make.ToLower().Contains(term) ||
				v.Model.ToLower().Contains(term));
		}

		return await query.Skip(skip).Take(take).ToListAsync();
	}
}
