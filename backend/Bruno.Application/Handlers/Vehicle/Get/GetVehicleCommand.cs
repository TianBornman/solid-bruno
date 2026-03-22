using Bruno.Application.DTOs.Vehicle;
using MediatR;

namespace Bruno.Application.Handlers.Vehicle.Get;

public record GetVehicleCommand(Guid Id) : IRequest<GetVehicleDto?>;
