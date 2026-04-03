using Bruno.Features.Booking.Models;
using Bruno.Shared.Api;
using Bruno.Shared.Models;

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

    public async Task<IReadOnlyList<BookingDto>> GetBookings(ListRequest request, CancellationToken ct = default)
    {
        return await api.PostAsync<ListRequest, List<BookingDto>>(Endpoints.List, request, ct) ?? [];
    }

    public async Task UpdateBooking(UpdateBookingRequest request, CancellationToken ct = default)
    {
        await api.PutAsync<UpdateBookingRequest, object>(Endpoints.Base, request, ct);
    }
}
