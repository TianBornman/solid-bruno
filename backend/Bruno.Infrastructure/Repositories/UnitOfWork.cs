using Bruno.Domain.Repositories;
using Bruno.Infrastructure.Context;

namespace Bruno.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
	private readonly BrunoContext dbContext;

	public ICustomerRepository CustomerRepository => new CustomerRepository(dbContext);

	public UnitOfWork(BrunoContext dbContext)
	{
		this.dbContext = dbContext;
	}

	public void Dispose()
	{
		dbContext.Dispose();
	}

	public async Task SaveChanges()
	{
		await dbContext.SaveChangesAsync();
	}
}
