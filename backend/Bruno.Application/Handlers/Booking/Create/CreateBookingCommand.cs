using Bruno.Domain.Enums;
using MediatR;

namespace Bruno.Application.Handlers.Booking.Create;

public record CreateBookingCommand(DateTime StartDate,
							DateTime EndDate,
							decimal TotalPrice,
							BookingStatus Status,
							Guid VehicleId,
							Guid CustomerId) : IRequest<Guid>;
