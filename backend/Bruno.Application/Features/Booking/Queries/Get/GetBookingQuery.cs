using Bruno.Application.DTOs.Booking;
using MediatR;

namespace Bruno.Application.Features.Booking.Queries.Get;

public record GetBookingQuery(Guid Id) : IRequest<GetBookingDto?>;
