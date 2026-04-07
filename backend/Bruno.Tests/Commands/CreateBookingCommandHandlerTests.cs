using Bruno.Application.Features.Booking.Commands.Create;
using Bruno.Domain.Entities;
using Bruno.Domain.Enums;
using Bruno.Domain.Exceptions;
using Bruno.Domain.Repositories;
using Bruno.Domain.ValueObjects;
using Moq;

namespace Bruno.Tests.Commands;

public class CreateBookingCommandHandlerTests
{
	private readonly Mock<IUnitOfWork> uow;
	private readonly Mock<IVehicleRepository> vehicleRepository;
	private readonly Mock<IBookingRepository> bookingRepository;
	private readonly CreateBookingCommandHandler handler;

	public CreateBookingCommandHandlerTests()
	{
		uow = new Mock<IUnitOfWork>();
		vehicleRepository = new Mock<IVehicleRepository>();
		bookingRepository = new Mock<IBookingRepository>();

		uow.Setup(u => u.VehicleRepository).Returns(vehicleRepository.Object);
		uow.Setup(u => u.BookingRepository).Returns(bookingRepository.Object);

		handler = new CreateBookingCommandHandler(uow.Object);
	}

	[Fact]
	public async Task Handle_WhenVehicleExistsAndNoOverlap_ReturnsNewBookingId()
	{
		var vehicle = new Vehicle
		{
			RegistrationNumber = "AB12CDE",
			Make = "Toyota",
			Model = "Corolla",
			Year = 2020,
			DailyRate = 50m
		};

		var command = new CreateBookingCommand(
			StartDate: DateTime.UtcNow.AddDays(1),
			EndDate: DateTime.UtcNow.AddDays(3),
			TotalPrice: 100m,
			Status: BookingStatus.Active,
			VehicleId: vehicle.Id,
			CustomerId: Guid.NewGuid());

		vehicleRepository.Setup(r => r.Get(vehicle.Id)).ReturnsAsync(vehicle);
		bookingRepository.Setup(r => r.HasOverlappingBookingAsync(vehicle.Id, It.IsAny<DateRange>())).ReturnsAsync(false);
		bookingRepository.Setup(r => r.Add(It.IsAny<Booking>())).Returns(Task.CompletedTask);

		var result = await handler.Handle(command, CancellationToken.None);

		Assert.NotEqual(Guid.Empty, result);
	}

	[Fact]
	public async Task Handle_WhenBookingOverlaps_ThrowsDomainException()
	{
		var vehicle = new Vehicle
		{
			RegistrationNumber = "XY99ZZZ",
			Make = "Ford",
			Model = "Focus",
			Year = 2021,
			DailyRate = 60m
		};

		var command = new CreateBookingCommand(
			StartDate: DateTime.UtcNow.AddDays(1),
			EndDate: DateTime.UtcNow.AddDays(3),
			TotalPrice: 120m,
			Status: BookingStatus.Active,
			VehicleId: vehicle.Id,
			CustomerId: Guid.NewGuid());

		vehicleRepository.Setup(r => r.Get(vehicle.Id)).ReturnsAsync(vehicle);
		bookingRepository.Setup(r => r.HasOverlappingBookingAsync(vehicle.Id, It.IsAny<DateRange>())).ReturnsAsync(true);

		await Assert.ThrowsAsync<DomainException>(() =>
			handler.Handle(command, CancellationToken.None));
	}
}
