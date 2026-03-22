using Bruno.Application.DTOs.Vehicle;
using MediatR;

namespace Bruno.Application.Handlers.Vehicle.List;

public record ListVehicleCommand(int Skip, int Take) : IRequest<List<GetVehicleDto>>;
