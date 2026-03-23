using Bruno.Domain.Enums;

namespace Bruno.Application.DTOs.Booking;

public record CreateBookingDto(DateTime StartDate,
							DateTime EndDate,
							decimal TotalPrice,
							BookingStatus Status,
							Guid VehicleId,
							Guid CustomerId);
