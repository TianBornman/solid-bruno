namespace Bruno.Domain.Repositories;

public interface IReadRepository<T>
{
	Task<T?> Get(Guid id);
	Task<IEnumerable<T>> List(int skip, int take);
}
