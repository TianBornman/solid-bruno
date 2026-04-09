using Bruno.Application.DTOs.Vehicle;
using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Features.Vehicle.Queries.List;

public class ListVehicleQueryHandler : IRequestHandler<ListVehicleQuery, List<GetVehicleDto>>
{
	private readonly IUnitOfWork uow;

	public ListVehicleQueryHandler(IUnitOfWork uow)
	{
		this.uow = uow;
	}

	public async Task<List<GetVehicleDto>> Handle(ListVehicleQuery request, CancellationToken cancellationToken)
	{
		var entities = await uow.VehicleRepository.ListFiltered(request.Skip, request.Take, request.Search);

		return entities.Select(entity => new GetVehicleDto(entity.Id, entity.RegistrationNumber, entity.Make, entity.Model,
									entity.Year, entity.DailyRate, entity.CreatedAt, entity.IsDeleted)).ToList();
	}
}
