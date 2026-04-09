using Bruno.Application.Events;
using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Features.Vehicle.Commands.Create;

public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, Guid>
{
	private readonly IUnitOfWork uow;
	private readonly IPublisher publisher;

	public CreateVehicleCommandHandler(IUnitOfWork uow, IPublisher publisher)
	{
		this.uow = uow;
		this.publisher = publisher;
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

		await publisher.Publish(new VehicleCreatedEvent(entity.Id, entity.RegistrationNumber), cancellationToken);

		return entity.Id;
	}
}
