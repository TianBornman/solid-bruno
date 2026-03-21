using Bruno.Domain.Entities;
using Bruno.Domain.Repositories;
using Bruno.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Bruno.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
	private readonly BrunoContext dbContext;

	public CustomerRepository(BrunoContext dbContext)
	{
		this.dbContext = dbContext;
	}

	public async Task Add(Customer entity)
	{
		await dbContext.Customers.AddAsync(entity);
		await dbContext.SaveChangesAsync();
	}

	public async Task Update(Customer entity)
	{
		dbContext.Update(entity);
		await dbContext.SaveChangesAsync();
	}

	public async Task Delete(Customer entity)
	{
		dbContext.Customers.Remove(entity);
		await dbContext.SaveChangesAsync();
	}

	public async Task<Customer?> Get(Guid id)
	{
		return await dbContext.Customers.SingleAsync(user => user.Id == id);
	}

	public async Task<IEnumerable<Customer>> List(int skip, int take)
	{
		return await dbContext.Customers.Skip(skip).Take(take).ToListAsync();
	}
}
