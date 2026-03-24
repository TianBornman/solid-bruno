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

	public void CanDelete()
	{
		if (!DateRange.IsInFuture())
			throw new DomainException("Only future bookings can be deleted.");

		if (DateRange.IsInPast())
			throw new DomainException("Past bookings cannot be deleted.");
	}
}
