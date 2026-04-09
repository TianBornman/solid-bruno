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
		return await dbContext.Customers.FirstOrDefaultAsync(customer => customer.Id == id);
	}

	public async Task<IEnumerable<Customer>> List(int skip, int take)
	{
		return await dbContext.Customers.Skip(skip).Take(take).ToListAsync();
	}

	public async Task<IEnumerable<Customer>> ListFiltered(int skip, int take, string? search)
	{
		var query = dbContext.Customers.AsQueryable();

		if (!string.IsNullOrWhiteSpace(search))
		{
			var term = search.ToLower();
			query = query.Where(c =>
				c.FirstName.ToLower().Contains(term) ||
				c.LastName.ToLower().Contains(term) ||
				c.Email.ToLower().Contains(term));
		}

		return await query.Skip(skip).Take(take).ToListAsync();
	}
}
