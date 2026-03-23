using Bruno.Application.DTOs.Booking;
using MediatR;

namespace Bruno.Application.Handlers.Booking.Get;

public record GetBookingCommand(Guid Id) : IRequest<GetBookingDto?>;
