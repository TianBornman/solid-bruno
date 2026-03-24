using Bruno.Domain.Exceptions;
using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Features.Booking.Commands.Update;

public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand>
{
	private readonly IUnitOfWork uow;

	public UpdateBookingCommandHandler(IUnitOfWork uow)
	{
		this.uow = uow;
	}

	public async Task Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
	{
		var entity = await uow.BookingRepository.Get(request.Id) 
			?? throw new NotFoundException($"Booking not found.");

		entity.DateRange = new(request.StartDate, request.EndDate);
		entity.TotalPrice = request.TotalPrice;
		entity.Status = request.Status;
		entity.VehicleId = request.VehicleId;
		entity.CustomerId = request.CustomerId;

		await uow.BookingRepository.Update(entity);
	}
}
