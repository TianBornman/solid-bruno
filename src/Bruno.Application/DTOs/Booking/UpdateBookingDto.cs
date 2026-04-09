using Bruno.Domain.Enums;

namespace Bruno.Application.DTOs.Booking;

public record UpdateBookingDto(Guid Id,
							DateTime StartDate,
							DateTime EndDate,
							decimal TotalPrice,
							BookingStatus Status,
							Guid VehicleId,
							Guid CustomerId);
