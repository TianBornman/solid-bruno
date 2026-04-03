using Bruno.Features.Booking.Models;
using Bruno.Shared.Models;

namespace Bruno.Features.Booking.Api;

public interface IBookingApi
{
    Task<Guid?> CreateBooking(CreateBookingRequest request, CancellationToken ct = default);
    Task DeleteBooking(Guid id, CancellationToken ct = default);
    Task<BookingDto?> GetBooking(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<BookingDto>> GetBookings(ListRequest request, CancellationToken ct = default);
    Task UpdateBooking(UpdateBookingRequest request, CancellationToken ct = default);
}
