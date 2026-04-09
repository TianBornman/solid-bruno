using MediatR;

namespace Bruno.Application.Events;

public record BookingCancelledEvent(Guid BookingId) : INotification;
