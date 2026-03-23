using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Handlers.Booking.Create;

public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Guid>
{
	private readonly IUnitOfWork uow;

	public CreateBookingCommandHandler(IUnitOfWork uow)
	{
		this.uow = uow;
	}

	public async Task<Guid> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
	{
		var existingVehicle = await uow.VehicleRepository.Get(request.VehicleId) 
			?? throw new Exception($"Vehicle {request.VehicleId} doesn't exist!");

		var existingCustomer = await uow.CustomerRepository.Get(request.CustomerId)
			?? throw new Exception($"Customer {request.CustomerId} doesn't exist!");

		var entity = new Domain.Entities.Booking
		{
			StartDate = request.StartDate,
			EndDate = request.EndDate,
			TotalPrice = request.TotalPrice,
			Status = request.Status,
			Vehicle = existingVehicle,
			Customer = existingCustomer,
		};

		await uow.BookingRepository.Add(entity);

		return entity.Id;
	}
}
