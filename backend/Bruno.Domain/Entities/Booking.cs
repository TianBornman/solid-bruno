using Bruno.Domain.Enums;

namespace Bruno.Domain.Entities;

public class Booking : BaseEntity
{
	public required DateTime StartDate { get; set; }
	public required DateTime EndDate { get; set; }
	public decimal TotalPrice { get; set; } = 0;
	public BookingStatus Status { get; set; } = 0;

	// Relationships
	public required Vehicle Vehicle { get; set; }
	public required Customer Customer { get; set; }
}
