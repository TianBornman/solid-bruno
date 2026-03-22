using MediatR;

namespace Bruno.Application.Handlers.Vehicle.Delete;

public record DeleteVehicleCommand(Guid Id) : IRequest;