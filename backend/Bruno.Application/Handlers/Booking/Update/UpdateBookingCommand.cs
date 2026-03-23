using Bruno.Domain.Enums;
using MediatR;

namespace Bruno.Application.Handlers.Booking.Update;

public record UpdateBookingCommand(Guid Id,
							DateTime StartDate,
							DateTime EndDate,
							decimal TotalPrice,
							BookingStatus Status,
							Guid VehicleId,
							Guid CustomerId) : IRequest;
