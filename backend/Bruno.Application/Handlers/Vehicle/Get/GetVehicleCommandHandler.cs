using Bruno.Application.DTOs.Vehicle;
using Bruno.Application.Handlers.Vehicle.Create;
using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Handlers.Vehicle.Get;

public class GetVehicleCommandHandler : IRequestHandler<GetVehicleCommand, GetVehicleDto?>
{
	private readonly IUnitOfWork uow;

	public GetVehicleCommandHandler(IUnitOfWork uow)
	{
		this.uow = uow;
	}

	public async Task<GetVehicleDto?> Handle(GetVehicleCommand request, CancellationToken cancellationToken)
	{
		var entity = await uow.VehicleRepository.Get(request.Id);

		var dto = entity != null ? new GetVehicleDto(entity.Id, entity.RegistrationNumber, entity.Make, entity.Model,
									entity.Year, entity.DailyRate, entity.CreatedAt, entity.IsDeleted) : null;

		return dto;
	}
}
