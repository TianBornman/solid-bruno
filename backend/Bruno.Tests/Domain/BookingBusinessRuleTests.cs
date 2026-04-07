using Bruno.Domain.Entities;
using Bruno.Domain.Enums;
using Bruno.Domain.Exceptions;
using Bruno.Domain.ValueObjects;

namespace Bruno.Tests.Domain;

public class BookingBusinessRuleTests
{
	[Fact]
	public void CanDelete_WhenBookingIsInPast_ThrowsDomainException()
	{
		var booking = new Booking
		{
			DateRange = new DateRange(DateTime.UtcNow.AddDays(-10), DateTime.UtcNow.AddDays(-5)),
			TotalPrice = 200m,
			Status = BookingStatus.Active,
			VehicleId = Guid.NewGuid(),
			CustomerId = Guid.NewGuid()
		};

		Assert.Throws<DomainException>(() => booking.CanDelete());
	}
}
