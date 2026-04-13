using Bruno.Application.DTOs.Vehicle;
using Bruno.Domain.Exceptions;
using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Features.Vehicle.Queries.Get;

public class GetVehicleQueryHandler : IRequestHandler<GetVehicleQuery, GetVehicleDto?>
{
	private readonly IUnitOfWork uow;

	public GetVehicleQueryHandler(IUnitOfWork uow)
	{
		this.uow = uow;
	}

	public async Task<GetVehicleDto?> Handle(GetVehicleQuery request, CancellationToken cancellationToken)
	{
		var entity = await uow.VehicleRepository.Get(request.Id) 
			?? throw new NotFoundException("Vehicle not found.");

		return GetVehicleDto.FromEntity(entity);
	}
}
