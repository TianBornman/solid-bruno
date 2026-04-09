using Bruno.Application.DTOs.Booking;
using Bruno.Domain.Enums;
using MediatR;

namespace Bruno.Application.Features.Booking.Queries.List;

public record ListBookingQuery(int Skip, int Take, BookingStatus? Status, string? Search) : IRequest<List<GetBookingDto>>;
