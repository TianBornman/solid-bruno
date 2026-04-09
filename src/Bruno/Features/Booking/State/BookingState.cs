using Bruno.Features.Booking.Api;
using Bruno.Features.Booking.Models;
using Bruno.Shared.Models;

namespace Bruno.Features.Booking.State;

public class BookingState
{
    public IReadOnlyList<BookingDto> Bookings { get; private set; } = [];
    public BookingDto? SelectedBooking { get; private set; }
    public bool IsLoading { get; private set; }
    public bool IsSaving { get; private set; }
    public string? Error { get; private set; }

    public event Action? OnChange;

    private readonly IBookingApi api;

    public BookingState(IBookingApi api)
    {
        this.api = api;
    }

    public async Task LoadBookings(CancellationToken ct = default)
    {
        SetLoading(true);
        try
        {
            Bookings = await api.GetBookings(new ListBookingRequest(0, 100, null, null), ct);
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

    public async Task LoadBooking(Guid id, CancellationToken ct = default)
    {
        SetLoading(true);
        try
        {
            SelectedBooking = await api.GetBooking(id, ct);
        }
        catch (Exception ex)
        {
            Error = ex.Message;
            SelectedBooking = null;
        }
        finally
        {
            SetLoading(false);
        }
    }

    public async Task<Guid?> CreateBooking(CreateBookingRequest request, CancellationToken ct = default)
    {
        SetSaving(true);
        try
        {
            return await api.CreateBooking(request, ct);
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

    public async Task<bool> UpdateBooking(UpdateBookingRequest request, CancellationToken ct = default)
    {
        SetSaving(true);
        try
        {
            await api.UpdateBooking(request, ct);
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

    public async Task<bool> DeleteBooking(Guid id, CancellationToken ct = default)
    {
        Error = null;
        try
        {
            await api.DeleteBooking(id, ct);
            Bookings = Bookings.Where(b => b.Id != id).ToList();
            if (SelectedBooking?.Id == id) SelectedBooking = null;
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
