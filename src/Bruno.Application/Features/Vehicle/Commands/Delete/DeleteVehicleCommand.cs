using MediatR;

namespace Bruno.Application.Features.Vehicle.Commands.Delete;

public record DeleteVehicleCommand(Guid Id) : IRequest;