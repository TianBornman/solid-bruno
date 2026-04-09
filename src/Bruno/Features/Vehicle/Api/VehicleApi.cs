using Bruno.Features.Vehicle.Models;
using Bruno.Shared.Api;

namespace Bruno.Features.Vehicle.Api;

public class VehicleApi : IVehicleApi
{
    private readonly IApiClient api;

    public VehicleApi(IApiClient api)
    {
        this.api = api;
    }

    public async Task<Guid?> CreateVehicle(CreateVehicleRequest request, CancellationToken ct = default)
    {
        return await api.PostAsync<CreateVehicleRequest, Guid>(Endpoints.Base, request, ct);
    }

    public async Task DeleteVehicle(Guid id, CancellationToken ct = default)
    {
        await api.DeleteAsync($"{Endpoints.Base}/{id}", ct);
    }

    public async Task<VehicleDto?> GetVehicle(Guid id, CancellationToken ct = default)
    {
        return await api.GetAsync<VehicleDto>($"{Endpoints.Base}/{id}", ct);
    }

    public async Task<IReadOnlyList<VehicleDto>> GetVehicles(ListVehicleRequest request, CancellationToken ct = default)
    {
        return await api.PostAsync<ListVehicleRequest, List<VehicleDto>>(Endpoints.List, request, ct) ?? [];
    }

    public async Task UpdateVehicle(UpdateVehicleRequest request, CancellationToken ct = default)
    {
        await api.PutAsync<UpdateVehicleRequest, object>(Endpoints.Base, request, ct);
    }
}
