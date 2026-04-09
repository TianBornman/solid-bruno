using MediatR;
using Microsoft.Extensions.Logging;

namespace Bruno.Application.Events.Handlers;

public class VehicleCreatedEventHandler : INotificationHandler<VehicleCreatedEvent>
{
    private readonly ILogger<VehicleCreatedEventHandler> logger;

    public VehicleCreatedEventHandler(ILogger<VehicleCreatedEventHandler> logger)
    {
        this.logger = logger;
    }

    public Task Handle(VehicleCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Vehicle created: {VehicleId} ({RegistrationNumber})",
            notification.VehicleId, notification.RegistrationNumber);

        return Task.CompletedTask;
    }
}
