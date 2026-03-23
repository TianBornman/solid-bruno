using Bruno.Domain.Enums;

namespace Bruno.Application.DTOs.Booking;

public record GetBookingDto(Guid Id,
							DateTime StartDate,
							DateTime EndDate,
							decimal TotalPrice,
							BookingStatus Status,
							Guid VehicleId,
							Guid CustomerId,
							DateTime CreatedDate);
