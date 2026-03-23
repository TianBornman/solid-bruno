using MediatR;

namespace Bruno.Application.Handlers.Booking.Delete;

public record DeleteBookingCommand(Guid Id) : IRequest;