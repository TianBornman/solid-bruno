using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Features.Vehicle.Commands.Create;

public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, Guid>
{
	private readonly IUnitOfWork uow;

	public CreateVehicleCommandHandler(IUnitOfWork uow)
	{
		this.uow = uow;
	}

	public async Task<Guid> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
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

		return entity.Id;
	}
}
