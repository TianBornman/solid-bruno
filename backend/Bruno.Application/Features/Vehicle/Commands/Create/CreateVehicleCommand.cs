using MediatR;

namespace Bruno.Application.Features.Vehicle.Commands.Create;

public record CreateVehicleCommand(string RegistrationNumber,
									string Make,
									string Model,
									int Year,
									decimal DailyRate) : IRequest<Guid>;
