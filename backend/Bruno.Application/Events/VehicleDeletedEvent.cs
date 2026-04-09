using MediatR;

namespace Bruno.Application.Events;

public record VehicleDeletedEvent(Guid VehicleId) : INotification;
