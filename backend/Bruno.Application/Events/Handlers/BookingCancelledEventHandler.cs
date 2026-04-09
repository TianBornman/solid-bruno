using MediatR;
using Microsoft.Extensions.Logging;

namespace Bruno.Application.Events.Handlers;

public class BookingCancelledEventHandler : INotificationHandler<BookingCancelledEvent>
{
    private readonly ILogger<BookingCancelledEventHandler> logger;

    public BookingCancelledEventHandler(ILogger<BookingCancelledEventHandler> logger)
    {
        this.logger = logger;
    }

    public Task Handle(BookingCancelledEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Booking cancelled: {BookingId}", notification.BookingId);

        return Task.CompletedTask;
    }
}
