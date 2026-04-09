namespace Bruno.Features.Booking.Models;

public record CreateBookingRequest(
    DateTime StartDate,
    DateTime EndDate,
    decimal TotalPrice,
    BookingStatus Status,
    Guid VehicleId,
    Guid CustomerId);
