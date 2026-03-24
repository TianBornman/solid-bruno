using Bruno.Application.DTOs.Booking;
using MediatR;

namespace Bruno.Application.Features.Booking.Queries.List;

public record ListBookingQuery(int Skip, int Take) : IRequest<List<GetBookingDto>>;
