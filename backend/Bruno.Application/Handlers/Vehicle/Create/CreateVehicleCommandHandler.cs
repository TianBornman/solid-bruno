using Bruno.Application.DTOs.Vehicle;
using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Handlers.Vehicle.Create;

public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, GetVehicleDto>
{
	private readonly IUnitOfWork uow;

	public CreateVehicleCommandHandler(IUnitOfWork uow)
	{
		this.uow = uow;
	}

	public async Task<GetVehicleDto> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
	{
		var entity = new Domain.Entities.Vehicle
		{
			RegistrationNumber = request.RegistrationNumber,
			Make = request.Make,
			Model = request.Model,
			Year = request.Year,
			DailyRate = request.DailyRate
		};

		await uow.VehicleRepository.Add(entity);

		var dto = new GetVehicleDto(entity.Id, entity.RegistrationNumber, entity.Make, entity.Model, 
									entity.Year, entity.DailyRate, entity.CreatedAt, entity.IsDeleted);

		return dto;
	}
}
