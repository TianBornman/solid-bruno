namespace Bruno.Domain.Repositories;

public interface IRepository<T>
{
	Task Add(T entity);
	Task Update(T entity);
	Task Delete(T entity);
	Task<T?> Get(Guid id);
	Task<IEnumerable<T>> List(int skip, int take);
}
