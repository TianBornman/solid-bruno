using Bruno.Application.Features.Vehicle.Commands.Update;
using Bruno.Domain.Entities;
using Bruno.Domain.Exceptions;
using Bruno.Domain.Repositories;
using Moq;

namespace Bruno.Tests.Commands;

public class UpdateVehicleCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> uow;
    private readonly Mock<IVehicleRepository> vehicleRepository;
    private readonly UpdateVehicleCommandHandler handler;

    public UpdateVehicleCommandHandlerTests()
    {
        uow = new Mock<IUnitOfWork>();
        vehicleRepository = new Mock<IVehicleRepository>();

        uow.Setup(u => u.VehicleRepository).Returns(vehicleRepository.Object);

        handler = new UpdateVehicleCommandHandler(uow.Object);
    }

    [Fact]
    public async Task Handle_WhenVehicleExists_UpdatesVehicle()
    {
        var vehicle = new Vehicle
        {
            RegistrationNumber = "OLD1234",
            Make = "Ford",
            Model = "Focus",
            Year = 2018,
            DailyRate = 40m
        };

        var command = new UpdateVehicleCommand(vehicle.Id, "NEW5678", "BMW", "3 Series", 2023, 120m);

        vehicleRepository.Setup(r => r.Get(vehicle.Id)).ReturnsAsync(vehicle);
        vehicleRepository.Setup(r => r.Update(vehicle)).Returns(Task.CompletedTask);

        await handler.Handle(command, CancellationToken.None);

        Assert.Equal("NEW5678", vehicle.RegistrationNumber);
        Assert.Equal("BMW", vehicle.Make);
        uow.Verify(u => u.SaveChanges(), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenVehicleNotFound_ThrowsNotFoundException()
    {
        var missingId = Guid.NewGuid();

        vehicleRepository.Setup(r => r.Get(missingId)).ReturnsAsync((Vehicle?)null);

        var command = new UpdateVehicleCommand(missingId, "XY99ZZZ", "Honda", "Jazz", 2021, 60m);

        await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(command, CancellationToken.None));
    }
}
