using Bruno.Domain.Exceptions;
using Bruno.Domain.Repositories;
using Bruno.Domain.ValueObjects;
using MediatR;

namespace Bruno.Application.Features.Booking.Commands.Create;

public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Guid>
{
	private readonly IUnitOfWork uow;

	public CreateBookingCommandHandler(IUnitOfWork uow)
	{
		this.uow = uow;
	}

	public async Task<Guid> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
	{
		var vehicle = await uow.VehicleRepository.Get(request.VehicleId) 
			?? throw new NotFoundException($"Vehicle not found.");

		vehicle.CanBook();

		var dateRange = new DateRange(request.StartDate, request.EndDate);

		var entity = new Domain.Entities.Booking
		{
			DateRange = dateRange,
			TotalPrice = request.TotalPrice,
			Status = request.Status,
			VehicleId = request.VehicleId,
			CustomerId = request.CustomerId
		};

		var hasOverlap = await uow.BookingRepository.HasOverlappingBookingAsync(request.VehicleId, dateRange);

		if (hasOverlap)
			throw new DomainException("Cannot overlap bookings for the same vehicle.");

		await uow.BookingRepository.Add(entity);

		return entity.Id;
	}
}
