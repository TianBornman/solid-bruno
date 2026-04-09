using MediatR;
using Microsoft.Extensions.Logging;

namespace Bruno.Application.Events.Handlers;

public class VehicleDeletedEventHandler : INotificationHandler<VehicleDeletedEvent>
{
    private readonly ILogger<VehicleDeletedEventHandler> logger;

    public VehicleDeletedEventHandler(ILogger<VehicleDeletedEventHandler> logger)
    {
        this.logger = logger;
    }

    public Task Handle(VehicleDeletedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Vehicle soft-deleted: {VehicleId}", notification.VehicleId);

        return Task.CompletedTask;
    }
}
