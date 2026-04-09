using Bruno.Features.Vehicle.Models;

namespace Bruno.Features.Vehicle.Api;

public interface IVehicleApi
{
    Task<Guid?> CreateVehicle(CreateVehicleRequest request, CancellationToken ct = default);
    Task DeleteVehicle(Guid id, CancellationToken ct = default);
    Task<VehicleDto?> GetVehicle(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<VehicleDto>> GetVehicles(ListVehicleRequest request, CancellationToken ct = default);
    Task UpdateVehicle(UpdateVehicleRequest request, CancellationToken ct = default);
}
