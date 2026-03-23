using Bruno.Application.DTOs.Booking;
using Bruno.Application.DTOs.Vehicle;
using MediatR;

namespace Bruno.Application.Handlers.Booking.List;

public record ListBookingCommand(int Skip, int Take) : IRequest<List<GetBookingDto>>;
