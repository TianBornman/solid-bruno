using Bruno.Application.Features.Vehicle.Commands.Delete;
using Bruno.Domain.Entities;
using Bruno.Domain.Exceptions;
using Bruno.Domain.Repositories;
using MediatR;
using Moq;

namespace Bruno.Tests.Commands;

public class DeleteVehicleCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> uow;
    private readonly Mock<IVehicleRepository> vehicleRepository;
    private readonly Mock<IPublisher> publisher;
    private readonly DeleteVehicleCommandHandler handler;

    public DeleteVehicleCommandHandlerTests()
    {
        uow = new Mock<IUnitOfWork>();
        vehicleRepository = new Mock<IVehicleRepository>();
        publisher = new Mock<IPublisher>();

        uow.Setup(u => u.VehicleRepository).Returns(vehicleRepository.Object);

        handler = new DeleteVehicleCommandHandler(uow.Object, publisher.Object);
    }

    [Fact]
    public async Task Handle_WhenVehicleExists_SoftDeletesAndPublishesEvent()
    {
        var vehicle = new Vehicle
        {
            RegistrationNumber = "AB12CDE",
            Make = "Honda",
            Model = "Civic",
            Year = 2020,
            DailyRate = 50m
        };

        vehicleRepository.Setup(r => r.Get(vehicle.Id)).ReturnsAsync(vehicle);
        vehicleRepository.Setup(r => r.SoftDelete(vehicle)).Returns(Task.CompletedTask);

        var command = new DeleteVehicleCommand(vehicle.Id);

        await handler.Handle(command, CancellationToken.None);

        vehicleRepository.Verify(r => r.SoftDelete(vehicle), Times.Once);
        uow.Verify(u => u.SaveChanges(), Times.Once);
        publisher.Verify(p => p.Publish(It.IsAny<INotification>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenVehicleNotFound_ThrowsNotFoundException()
    {
        var missingId = Guid.NewGuid();

        vehicleRepository.Setup(r => r.Get(missingId)).ReturnsAsync((Vehicle?)null);

        var command = new DeleteVehicleCommand(missingId);

        await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(command, CancellationToken.None));
    }
}
