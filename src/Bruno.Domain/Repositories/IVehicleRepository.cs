using Bruno.Domain.Entities;

namespace Bruno.Domain.Repositories;

public interface IVehicleRepository : IRepository<Vehicle>
{
	Task<IEnumerable<Vehicle>> ListFiltered(int skip, int take, string? search);
}
