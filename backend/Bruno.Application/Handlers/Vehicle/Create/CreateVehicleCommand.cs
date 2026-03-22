using MediatR;

namespace Bruno.Application.Handlers.Vehicle.Create;

public record CreateVehicleCommand(string RegistrationNumber,
									string Make,
									string Model,
									int Year,
									decimal DailyRate) : IRequest<Guid>;
