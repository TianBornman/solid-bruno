using Bruno.Application.Events;
using Bruno.Domain.Exceptions;
using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Features.Vehicle.Commands.Delete;

public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand>
{
	private readonly IUnitOfWork uow;
	private readonly IPublisher publisher;

	public DeleteVehicleCommandHandler(IUnitOfWork uow, IPublisher publisher)
	{
		this.uow = uow;
		this.publisher = publisher;
	}

	public async Task Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
	{
		var entity = await uow.VehicleRepository.Get(request.Id)
			?? throw new NotFoundException("Vehicle not found.");

		await uow.VehicleRepository.Delete(entity);

		await publisher.Publish(new VehicleDeletedEvent(entity.Id), cancellationToken);
	}
}
