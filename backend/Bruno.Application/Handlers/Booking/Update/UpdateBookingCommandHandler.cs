using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Handlers.Booking.Update;

public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand>
{
	private readonly IUnitOfWork uow;

	public UpdateBookingCommandHandler(IUnitOfWork uow)
	{
		this.uow = uow;
	}

	public async Task Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
	{
		var entity = await uow.BookingRepository.Get(request.Id);

		if (entity == null)
			return;

		var existingVehicle = await uow.VehicleRepository.Get(request.VehicleId)
			?? throw new Exception($"Vehicle {request.VehicleId} doesn't exist!");

		var existingCustomer = await uow.CustomerRepository.Get(request.CustomerId)
			?? throw new Exception($"Customer {request.CustomerId} doesn't exist!");

		entity.StartDate = request.StartDate;
		entity.EndDate = request.EndDate;
		entity.TotalPrice = request.TotalPrice;
		entity.Status = request.Status;
		entity.Vehicle = existingVehicle;
		entity.Customer = existingCustomer;

		await uow.BookingRepository.Update(entity);
	}
}
