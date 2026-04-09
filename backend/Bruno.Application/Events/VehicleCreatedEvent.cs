using MediatR;

namespace Bruno.Application.Events;

public record VehicleCreatedEvent(Guid VehicleId, string RegistrationNumber) : INotification;
