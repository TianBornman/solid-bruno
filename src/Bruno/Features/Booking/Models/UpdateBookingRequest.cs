namespace Bruno.Features.Booking.Models;

public record UpdateBookingRequest(
    Guid Id,
    DateTime StartDate,
    DateTime EndDate,
    decimal TotalPrice,
    BookingStatus Status,
    Guid VehicleId,
    Guid CustomerId);
