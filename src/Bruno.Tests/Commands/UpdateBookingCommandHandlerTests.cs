using Bruno.Application.Features.Booking.Commands.Update;
using Bruno.Domain.Entities;
using Bruno.Domain.Enums;
using Bruno.Domain.Exceptions;
using Bruno.Domain.Repositories;
using Bruno.Domain.ValueObjects;
using MediatR;
using Moq;

namespace Bruno.Tests.Commands;

public class UpdateBookingCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> uow;
    private readonly Mock<IBookingRepository> bookingRepository;
    private readonly Mock<IPublisher> publisher;
    private readonly UpdateBookingCommandHandler handler;

    public UpdateBookingCommandHandlerTests()
    {
        uow = new Mock<IUnitOfWork>();
        bookingRepository = new Mock<IBookingRepository>();
        publisher = new Mock<IPublisher>();

        uow.Setup(u => u.BookingRepository).Returns(bookingRepository.Object);

        handler = new UpdateBookingCommandHandler(uow.Object, publisher.Object);
    }

    [Fact]
    public async Task Handle_WhenBookingNotFound_ThrowsNotFoundException()
    {
        var missingId = Guid.NewGuid();

        bookingRepository.Setup(r => r.Get(missingId)).ReturnsAsync((Booking?)null);

        var command = new UpdateBookingCommand(
            missingId,
            DateTime.UtcNow.AddDays(1),
            DateTime.UtcNow.AddDays(3),
            100m,
            BookingStatus.Active,
            Guid.NewGuid(),
            Guid.NewGuid());

        await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenOverlappingBookingExists_ThrowsDomainException()
    {
        var booking = new Booking
        {
            DateRange = new DateRange(DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(3)),
            TotalPrice = 100m,
            Status = BookingStatus.Active,
            VehicleId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid()
        };

        bookingRepository.Setup(r => r.Get(booking.Id)).ReturnsAsync(booking);
        bookingRepository
            .Setup(r => r.HasOverlappingBookingAsync(It.IsAny<Guid>(), It.IsAny<DateRange>(), booking.Id))
            .ReturnsAsync(true);

        var command = new UpdateBookingCommand(
            booking.Id,
            DateTime.UtcNow.AddDays(2),
            DateTime.UtcNow.AddDays(5),
            150m,
            BookingStatus.Active,
            booking.VehicleId,
            booking.CustomerId);

        await Assert.ThrowsAsync<DomainException>(() =>
            handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenValid_UpdatesBookingAndSaves()
    {
        var booking = new Booking
        {
            DateRange = new DateRange(DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(3)),
            TotalPrice = 100m,
            Status = BookingStatus.Active,
            VehicleId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid()
        };

        bookingRepository.Setup(r => r.Get(booking.Id)).ReturnsAsync(booking);
        bookingRepository
            .Setup(r => r.HasOverlappingBookingAsync(It.IsAny<Guid>(), It.IsAny<DateRange>(), booking.Id))
            .ReturnsAsync(false);
        bookingRepository.Setup(r => r.Update(booking)).Returns(Task.CompletedTask);

        var command = new UpdateBookingCommand(
            booking.Id,
            DateTime.UtcNow.AddDays(2),
            DateTime.UtcNow.AddDays(5),
            150m,
            BookingStatus.Active,
            booking.VehicleId,
            booking.CustomerId);

        await handler.Handle(command, CancellationToken.None);

        Assert.Equal(150m, booking.TotalPrice);
        uow.Verify(u => u.SaveChanges(), Times.Once);
        publisher.Verify(p => p.Publish(It.IsAny<INotification>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WhenStatusChangesToCancelled_PublishesCancelledEvent()
    {
        var booking = new Booking
        {
            DateRange = new DateRange(DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(3)),
            TotalPrice = 100m,
            Status = BookingStatus.Active,
            VehicleId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid()
        };

        bookingRepository.Setup(r => r.Get(booking.Id)).ReturnsAsync(booking);
        bookingRepository
            .Setup(r => r.HasOverlappingBookingAsync(It.IsAny<Guid>(), It.IsAny<DateRange>(), booking.Id))
            .ReturnsAsync(false);
        bookingRepository.Setup(r => r.Update(booking)).Returns(Task.CompletedTask);

        var command = new UpdateBookingCommand(
            booking.Id,
            DateTime.UtcNow.AddDays(1),
            DateTime.UtcNow.AddDays(3),
            100m,
            BookingStatus.Cancelled,
            booking.VehicleId,
            booking.CustomerId);

        await handler.Handle(command, CancellationToken.None);

        publisher.Verify(p => p.Publish(It.IsAny<INotification>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
