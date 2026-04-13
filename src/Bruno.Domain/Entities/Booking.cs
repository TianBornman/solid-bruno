using Bruno.Domain.Enums;
using Bruno.Domain.Exceptions;
using Bruno.Domain.ValueObjects;

namespace Bruno.Domain.Entities;

public class Booking : BaseEntity
{
	public required DateRange DateRange { get; set; }
	public required decimal TotalPrice { get; set; }
	public required BookingStatus Status { get; set; }

	public Guid VehicleId { get; set; }
	public Guid CustomerId { get; set; }

	// Relationships
	public Vehicle Vehicle { get; set; } = default!;
	public Customer Customer { get; set; } = default!;

	public bool Update(DateRange dateRange, decimal totalPrice, BookingStatus status, Guid vehicleId, Guid customerId)
	{
		var wasCancelled = Status != BookingStatus.Cancelled && status == BookingStatus.Cancelled;

		DateRange = dateRange;
		TotalPrice = totalPrice;
		Status = status;
		VehicleId = vehicleId;
		CustomerId = customerId;

		return wasCancelled;
	}

	public void CanDelete()
	{
		if (!DateRange.IsInFuture())
			throw new DomainException("Only future bookings can be deleted.");
	}
}
