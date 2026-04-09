using MediatR;

namespace Bruno.Application.Events;

public record BookingCreatedEvent(Guid BookingId, Guid VehicleId, Guid CustomerId) : INotification;
