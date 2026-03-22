using Bruno.Application.DTOs.Vehicle;
using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Handlers.Vehicle.List;

public class ListVehicleCommandHandler : IRequestHandler<ListVehicleCommand, List<GetVehicleDto>>
{
	private readonly IUnitOfWork uow;

	public ListVehicleCommandHandler(IUnitOfWork uow)
	{
		this.uow = uow;
	}

	public async Task<List<GetVehicleDto>> Handle(ListVehicleCommand request, CancellationToken cancellationToken)
	{
		var entities = await uow.VehicleRepository.List(request.Skip, request.Take);

		var dtos = entities.Any() ? entities.Select(entity => 
									new GetVehicleDto(entity.Id, entity.RegistrationNumber, entity.Make, entity.Model,
									entity.Year, entity.DailyRate, entity.CreatedAt, entity.IsDeleted)).ToList() : [];

		return dtos;
	}
}
