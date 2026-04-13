using Bruno.Domain.Enums;
using BookingEntity = Bruno.Domain.Entities.Booking;

namespace Bruno.Application.DTOs.Booking;

public record GetBookingDto(Guid Id,
							DateTime StartDate,
							DateTime EndDate,
							decimal TotalPrice,
							BookingStatus Status,
							Guid VehicleId,
							Guid CustomerId,
							DateTime CreatedDate)
{
	public static GetBookingDto FromEntity(BookingEntity entity) =>
		new(entity.Id, entity.DateRange.StartDate, entity.DateRange.EndDate, entity.TotalPrice, entity.Status,
			entity.Vehicle.Id, entity.Customer.Id, entity.CreatedAt);
}
