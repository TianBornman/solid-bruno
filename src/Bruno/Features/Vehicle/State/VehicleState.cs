using Bruno.Features.Vehicle.Api;
using Bruno.Features.Vehicle.Models;

namespace Bruno.Features.Vehicle.State;

public class VehicleState
{
    public IReadOnlyList<VehicleDto> Vehicles { get; private set; } = [];
    public VehicleDto? SelectedVehicle { get; private set; }
    public bool IsLoading { get; private set; }
    public bool IsSaving { get; private set; }
    public string? Error { get; private set; }

    public event Action? OnChange;

    private readonly IVehicleApi api;

    public VehicleState(IVehicleApi api)
    {
        this.api = api;
    }

    public async Task LoadVehicles(CancellationToken ct = default)
    {
        SetLoading(true);
        try
        {
            Vehicles = await api.GetVehicles(new ListVehicleRequest(0, 100, null), ct);
        }
        catch (Exception ex)
        {
            Error = ex.Message;
        }
        finally
        {
            SetLoading(false);
        }
    }

    public async Task LoadVehicle(Guid id, CancellationToken ct = default)
    {
        SetLoading(true);
        try
        {
            SelectedVehicle = await api.GetVehicle(id, ct);
        }
        catch (Exception ex)
        {
            Error = ex.Message;
            SelectedVehicle = null;
        }
        finally
        {
            SetLoading(false);
        }
    }

    public async Task<Guid?> CreateVehicle(CreateVehicleRequest request, CancellationToken ct = default)
    {
        SetSaving(true);
        try
        {
            return await api.CreateVehicle(request, ct);
        }
        catch (Exception ex)
        {
            Error = ex.Message;
            return null;
        }
        finally
        {
            SetSaving(false);
        }
    }

    public async Task<bool> UpdateVehicle(UpdateVehicleRequest request, CancellationToken ct = default)
    {
        SetSaving(true);
        try
        {
            await api.UpdateVehicle(request, ct);
            return true;
        }
        catch (Exception ex)
        {
            Error = ex.Message;
            return false;
        }
        finally
        {
            SetSaving(false);
        }
    }

    public async Task<bool> DeleteVehicle(Guid id, CancellationToken ct = default)
    {
        Error = null;
        try
        {
            await api.DeleteVehicle(id, ct);
            Vehicles = Vehicles.Where(v => v.Id != id).ToList();
            if (SelectedVehicle?.Id == id) SelectedVehicle = null;
            NotifyStateChanged();
            return true;
        }
        catch (Exception ex)
        {
            Error = ex.Message;
            NotifyStateChanged();
            return false;
        }
    }

    private void SetLoading(bool loading)
    {
        IsLoading = loading;
        if (loading) Error = null;
        NotifyStateChanged();
    }

    private void SetSaving(bool saving)
    {
        IsSaving = saving;
        if (saving) Error = null;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
