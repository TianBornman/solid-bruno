using Bruno.Domain.Exceptions;
using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Features.Vehicle.Commands.Delete;

public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand>
{
	private readonly IUnitOfWork uow;

	public DeleteVehicleCommandHandler(IUnitOfWork uow)
	{
		this.uow = uow;
	}

	public async Task Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
	{
		var entity = await uow.VehicleRepository.Get(request.Id) 
			?? throw new NotFoundException("Vehicle not found.");

		await uow.VehicleRepository.Delete(entity);
	}
}
