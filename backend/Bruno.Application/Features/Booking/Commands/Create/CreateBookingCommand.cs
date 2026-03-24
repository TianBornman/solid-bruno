using Bruno.Domain.Enums;
using MediatR;

namespace Bruno.Application.Features.Booking.Commands.Create;

public record CreateBookingCommand(DateTime StartDate,
							DateTime EndDate,
							decimal TotalPrice,
							BookingStatus Status,
							Guid VehicleId,
							Guid CustomerId) : IRequest<Guid>;
