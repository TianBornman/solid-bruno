using MediatR;

namespace Bruno.Application.Features.Booking.Commands.Delete;

public record DeleteBookingCommand(Guid Id) : IRequest;