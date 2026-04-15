using Bruno.Application.Features.Vehicle.Commands.Create;
using Bruno.Domain.Entities;
using Bruno.Domain.Repositories;
using MediatR;
using Moq;

namespace Bruno.Tests.Commands;

public class CreateVehicleCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> uow;
    private readonly Mock<IVehicleRepository> vehicleRepository;
    private readonly Mock<IPublisher> publisher;
    private readonly CreateVehicleCommandHandler handler;

    public CreateVehicleCommandHandlerTests()
    {
        uow = new Mock<IUnitOfWork>();
        vehicleRepository = new Mock<IVehicleRepository>();
        publisher = new Mock<IPublisher>();

        uow.Setup(u => u.VehicleRepository).Returns(vehicleRepository.Object);

        handler = new CreateVehicleCommandHandler(uow.Object, publisher.Object);
    }

    [Fact]
    public async Task Handle_CreatesVehicleAndReturnsNonEmptyId()
    {
        vehicleRepository.Setup(r => r.Add(It.IsAny<Vehicle>())).Returns(Task.CompletedTask);

        var command = new CreateVehicleCommand("AB12CDE", "Toyota", "Corolla", 2020, 55m);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.NotEqual(Guid.Empty, result);
        vehicleRepository.Verify(r => r.Add(It.IsAny<Vehicle>()), Times.Once);
        uow.Verify(u => u.SaveChanges(), Times.Once);
    }

    [Fact]
    public async Task Handle_PublishesVehicleCreatedEvent()
    {
        vehicleRepository.Setup(r => r.Add(It.IsAny<Vehicle>())).Returns(Task.CompletedTask);

        var command = new CreateVehicleCommand("AB12CDE", "Toyota", "Corolla", 2020, 55m);

        await handler.Handle(command, CancellationToken.None);

        publisher.Verify(p => p.Publish(It.IsAny<INotification>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
