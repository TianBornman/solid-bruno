using MediatR;
using Microsoft.Extensions.Logging;

namespace Bruno.Application.Events.Handlers;

public class BookingCreatedEventHandler : INotificationHandler<BookingCreatedEvent>
{
    private readonly ILogger<BookingCreatedEventHandler> logger;

    public BookingCreatedEventHandler(ILogger<BookingCreatedEventHandler> logger)
    {
        this.logger = logger;
    }

    public Task Handle(BookingCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Booking created: {BookingId} for Vehicle {VehicleId}, Customer {CustomerId}",
            notification.BookingId, notification.VehicleId, notification.CustomerId);

        return Task.CompletedTask;
    }
}
