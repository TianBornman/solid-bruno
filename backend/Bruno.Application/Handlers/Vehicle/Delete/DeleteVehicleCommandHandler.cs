using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Handlers.Vehicle.Delete;

public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand>
{
	private readonly IUnitOfWork uow;

	public DeleteVehicleCommandHandler(IUnitOfWork uow)
	{
		this.uow = uow;
	}

	public async Task Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
	{
		var entity = await uow.VehicleRepository.Get(request.Id);

		if (entity != null)
			await uow.VehicleRepository.Delete(entity);
	}
}
