namespace Bruno.Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
	public ICustomerRepository CustomerRepository { get; }

	public Task SaveChanges();
}
