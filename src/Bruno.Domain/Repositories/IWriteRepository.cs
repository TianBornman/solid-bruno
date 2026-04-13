namespace Bruno.Domain.Repositories;

public interface IWriteRepository<T>
{
	Task Add(T entity);
	Task Update(T entity);
	Task Delete(T entity);
}
