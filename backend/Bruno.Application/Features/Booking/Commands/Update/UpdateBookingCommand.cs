using Bruno.Domain.Enums;
using MediatR;

namespace Bruno.Application.Features.Booking.Commands.Update;

public record UpdateBookingCommand(Guid Id,
							DateTime StartDate,
							DateTime EndDate,
							decimal TotalPrice,
							BookingStatus Status,
							Guid VehicleId,
							Guid CustomerId) : IRequest;
