using Bruno.Application.Features.Booking.Commands.Delete;
using Bruno.Domain.Entities;
using Bruno.Domain.Enums;
using Bruno.Domain.Exceptions;
using Bruno.Domain.Repositories;
using Bruno.Domain.ValueObjects;
using Moq;

namespace Bruno.Tests.Commands;

public class DeleteBookingCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> uow;
    private readonly Mock<IBookingRepository> bookingRepository;
    private readonly DeleteBookingCommandHandler handler;

    public DeleteBookingCommandHandlerTests()
    {
        uow = new Mock<IUnitOfWork>();
        bookingRepository = new Mock<IBookingRepository>();

        uow.Setup(u => u.BookingRepository).Returns(bookingRepository.Object);

        handler = new DeleteBookingCommandHandler(uow.Object);
    }

    [Fact]
    public async Task Handle_WhenBookingNotFound_ThrowsNotFoundException()
    {
        var missingId = Guid.NewGuid();

        bookingRepository.Setup(r => r.Get(missingId)).ReturnsAsync((Booking?)null);

        var command = new DeleteBookingCommand(missingId);

        await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenBookingIsInFuture_DeletesSuccessfully()
    {
        var booking = new Booking
        {
            DateRange = new DateRange(DateTime.UtcNow.AddDays(2), DateTime.UtcNow.AddDays(5)),
            TotalPrice = 150m,
            Status = BookingStatus.Active,
            VehicleId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid()
        };

        bookingRepository.Setup(r => r.Get(booking.Id)).ReturnsAsync(booking);
        bookingRepository.Setup(r => r.Delete(booking)).Returns(Task.CompletedTask);

        var command = new DeleteBookingCommand(booking.Id);

        await handler.Handle(command, CancellationToken.None);

        bookingRepository.Verify(r => r.Delete(booking), Times.Once);
        uow.Verify(u => u.SaveChanges(), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenBookingIsInPast_ThrowsDomainException()
    {
        var booking = new Booking
        {
            DateRange = new DateRange(DateTime.UtcNow.AddDays(-10), DateTime.UtcNow.AddDays(-5)),
            TotalPrice = 200m,
            Status = BookingStatus.Active,
            VehicleId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid()
        };

        bookingRepository.Setup(r => r.Get(booking.Id)).ReturnsAsync(booking);

        var command = new DeleteBookingCommand(booking.Id);

        await Assert.ThrowsAsync<DomainException>(() =>
            handler.Handle(command, CancellationToken.None));
    }
}
