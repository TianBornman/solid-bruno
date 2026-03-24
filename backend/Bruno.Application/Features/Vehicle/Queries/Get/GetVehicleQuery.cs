using Bruno.Application.DTOs.Vehicle;
using MediatR;

namespace Bruno.Application.Features.Vehicle.Queries.Get;

public record GetVehicleQuery(Guid Id) : IRequest<GetVehicleDto?>;
