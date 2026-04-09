using Bruno.Application.DTOs.Vehicle;
using MediatR;

namespace Bruno.Application.Features.Vehicle.Queries.List;

public record ListVehicleQuery(int Skip, int Take, string? Search) : IRequest<List<GetVehicleDto>>;
