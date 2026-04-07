using Bruno.Application.Features.Vehicle.Queries.Get;
using Bruno.Domain.Entities;
using Bruno.Domain.Exceptions;
using Bruno.Domain.Repositories;
using Moq;

namespace Bruno.Tests.Queries;

public class GetVehicleQueryHandlerTests
{
	private readonly Mock<IUnitOfWork> uow;
	private readonly Mock<IVehicleRepository> vehicleRepository;
	private readonly GetVehicleQueryHandler handler;

	public GetVehicleQueryHandlerTests()
	{
		uow = new Mock<IUnitOfWork>();
		vehicleRepository = new Mock<IVehicleRepository>();

		uow.Setup(u => u.VehicleRepository).Returns(vehicleRepository.Object);

		handler = new GetVehicleQueryHandler(uow.Object);
	}

	[Fact]
	public async Task Handle_WhenVehicleExists_ReturnsVehicleDto()
	{
		var vehicle = new Vehicle
		{
			RegistrationNumber = "AB12CDE",
			Make = "Honda",
			Model = "Civic",
			Year = 2019,
			DailyRate = 45m
		};

		vehicleRepository.Setup(r => r.Get(vehicle.Id)).ReturnsAsync(vehicle);

		var query = new GetVehicleQuery(vehicle.Id);

		var result = await handler.Handle(query, CancellationToken.None);

		Assert.NotNull(result);
		Assert.Equal(vehicle.Id, result.Id);
		Assert.Equal("AB12CDE", result.RegistrationNumber);
		Assert.Equal("Honda", result.Make);
	}

	[Fact]
	public async Task Handle_WhenVehicleDoesNotExist_ThrowsNotFoundException()
	{
		var missingId = Guid.NewGuid();

		vehicleRepository.Setup(r => r.Get(missingId)).ReturnsAsync((Vehicle?)null);

		var query = new GetVehicleQuery(missingId);

		await Assert.ThrowsAsync<NotFoundException>(() =>
			handler.Handle(query, CancellationToken.None));
	}
}
