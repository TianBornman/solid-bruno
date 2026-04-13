using Bruno.Features.Booking.Models;
using Bruno.Shared.Api;

namespace Bruno.Features.Booking.Api;

public class BookingApi : IBookingApi
{
    private readonly IApiClient api;

    public BookingApi(IApiClient api)
    {
        this.api = api;
    }

    public async Task<Guid?> CreateBooking(CreateBookingRequest request, CancellationToken ct = default)
    {
        return await api.PostAsync<CreateBookingRequest, Guid>(Endpoints.Base, request, ct);
    }

    public async Task DeleteBooking(Guid id, CancellationToken ct = default)
    {
        await api.DeleteAsync($"{Endpoints.Base}/{id}", ct);
    }

    public async Task<BookingDto?> GetBooking(Guid id, CancellationToken ct = default)
    {
        return await api.GetAsync<BookingDto>($"{Endpoints.Base}/{id}", ct);
    }

    public async Task<IReadOnlyList<BookingDto>> GetBookings(ListBookingRequest request, CancellationToken ct = default)
    {
        var query = $"?skip={request.Skip}&take={request.Take}";
        if (request.Status.HasValue)
            query += $"&status={Uri.EscapeDataString(request.Status.Value.ToString())}";
        if (!string.IsNullOrWhiteSpace(request.Search))
            query += $"&search={Uri.EscapeDataString(request.Search)}";

        return await api.GetAsync<List<BookingDto>>($"{Endpoints.Base}{query}", ct) ?? [];
    }

    public async Task UpdateBooking(UpdateBookingRequest request, CancellationToken ct = default)
    {
        await api.PutAsync<UpdateBookingRequest, object>(Endpoints.Base, request, ct);
    }
}
