namespace Bruno.Domain.Repositories;

public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T> { }
