using MediatR;

namespace Bruno.Application.Features.Vehicle.Commands.Update;

public record UpdateVehicleCommand(Guid Id,
									string RegistrationNumber,
									string Make,
									string Model,
									int Year,
									decimal DailyRate) : IRequest;
