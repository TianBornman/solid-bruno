namespace Bruno.Features.Booking.Models;

public record BookingDto(Guid Id,
                    DateTime StartDate,
                    DateTime EndDate,
                    decimal TotalPrice,
                    BookingStatus Status,
                    Guid VehicleId,
                    Guid CustomerId,
                    DateTime CreatedDate);
