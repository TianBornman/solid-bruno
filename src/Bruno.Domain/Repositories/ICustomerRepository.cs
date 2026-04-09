using Bruno.Domain.Entities;

namespace Bruno.Domain.Repositories;

public interface ICustomerRepository : IRepository<Customer>
{
	Task<IEnumerable<Customer>> ListFiltered(int skip, int take, string? search);
}
