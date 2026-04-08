namespace Bruno.Features.Booking.Models;

public record ListBookingRequest(int Skip, int Take, BookingStatus? Status, string? Search);
