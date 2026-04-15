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

	[Fact]
	public void CanDelete_WhenBookingIsInFuture_DoesNotThrow()
	{
		var booking = new Booking
		{
			DateRange = new DateRange(DateTime.UtcNow.AddDays(2), DateTime.UtcNow.AddDays(5)),
			TotalPrice = 150m,
			Status = BookingStatus.Active,
			VehicleId = Guid.NewGuid(),
			CustomerId = Guid.NewGuid()
		};

		// Should not throw
		booking.CanDelete();
	}

	[Fact]
	public void Update_WhenStatusChangesToCancelled_ReturnsTrue()
	{
		var booking = new Booking
		{
			DateRange = new DateRange(DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(3)),
			TotalPrice = 100m,
			Status = BookingStatus.Active,
			VehicleId = Guid.NewGuid(),
			CustomerId = Guid.NewGuid()
		};

		var wasCancelled = booking.Update(booking.DateRange, 100m, BookingStatus.Cancelled, booking.VehicleId, booking.CustomerId);

		Assert.True(wasCancelled);
	}

	[Fact]
	public void Update_WhenStatusDoesNotChangeToCancelled_ReturnsFalse()
	{
		var booking = new Booking
		{
			DateRange = new DateRange(DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(3)),
			TotalPrice = 100m,
			Status = BookingStatus.Active,
			VehicleId = Guid.NewGuid(),
			CustomerId = Guid.NewGuid()
		};

		var wasCancelled = booking.Update(booking.DateRange, 120m, BookingStatus.Active, booking.VehicleId, booking.CustomerId);

		Assert.False(wasCancelled);
	}
}
