using Bruno.Features.Customer.Api;
using Bruno.Features.Customer.Models;

namespace Bruno.Features.Customer.State;

public class CustomerState
{
    public IReadOnlyList<CustomerDto> Customers { get; private set; } = [];
    public CustomerDto? SelectedCustomer { get; private set; }
    public bool IsLoading { get; private set; }
    public bool IsSaving { get; private set; }
    public string? Error { get; private set; }

    public event Action? OnChange;

    private readonly ICustomerApi _api;

    public CustomerState(ICustomerApi api)
    {
        _api = api;
    }

    public async Task LoadCustomers(CancellationToken ct = default)
    {
        SetLoading(true);
        try
        {
            Customers = await _api.GetCustomers(new ListCustomerRequest(0, 100, null), ct);
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

    public async Task LoadCustomer(Guid id, CancellationToken ct = default)
    {
        SetLoading(true);
        try
        {
            SelectedCustomer = await _api.GetCustomer(id, ct);
        }
        catch (Exception ex)
        {
            Error = ex.Message;
            SelectedCustomer = null;
        }
        finally
        {
            SetLoading(false);
        }
    }

    public async Task<Guid?> CreateCustomer(CreateCustomerRequest request, CancellationToken ct = default)
    {
        SetSaving(true);
        try
        {
            return await _api.CreateCustomer(request, ct);
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

    public async Task<bool> UpdateCustomer(UpdateCustomerRequest request, CancellationToken ct = default)
    {
        SetSaving(true);
        try
        {
            await _api.UpdateCustomer(request, ct);
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

    public async Task<bool> DeleteCustomer(Guid id, CancellationToken ct = default)
    {
        Error = null;
        try
        {
            await _api.DeleteCustomer(id, ct);
            Customers = Customers.Where(c => c.Id != id).ToList();
            if (SelectedCustomer?.Id == id) SelectedCustomer = null;
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
