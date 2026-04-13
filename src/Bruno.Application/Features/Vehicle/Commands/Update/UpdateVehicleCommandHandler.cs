using Bruno.Domain.Exceptions;
using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Features.Vehicle.Commands.Update;

public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand>
{
	private readonly IUnitOfWork uow;

	public UpdateVehicleCommandHandler(IUnitOfWork uow)
	{
		this.uow = uow;
	}

	public async Task Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
	{
		var entity = await uow.VehicleRepository.Get(request.Id) 
			?? throw new NotFoundException("Vehicle not found.");

		entity.Update(request.RegistrationNumber, request.Make, request.Model, request.Year, request.DailyRate);

		await uow.VehicleRepository.Update(entity);
		await uow.SaveChanges();
	}
}
