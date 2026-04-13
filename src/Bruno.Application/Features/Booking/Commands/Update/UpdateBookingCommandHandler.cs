using Bruno.Application.Events;
using Bruno.Domain.Exceptions;
using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Features.Booking.Commands.Update;

public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand>
{
	private readonly IUnitOfWork uow;
	private readonly IPublisher publisher;

	public UpdateBookingCommandHandler(IUnitOfWork uow, IPublisher publisher)
	{
		this.uow = uow;
		this.publisher = publisher;
	}

	public async Task Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
	{
		var entity = await uow.BookingRepository.Get(request.Id)
			?? throw new NotFoundException($"Booking not found.");

		var dateRange = new Domain.ValueObjects.DateRange(request.StartDate, request.EndDate);

		var hasOverlap = await uow.BookingRepository.HasOverlappingBookingAsync(request.VehicleId, dateRange, excludeBookingId: entity.Id);
		if (hasOverlap)
			throw new DomainException("Cannot overlap bookings for the same vehicle.");

		var wasCancelled = entity.Update(dateRange, request.TotalPrice, request.Status, request.VehicleId, request.CustomerId);

		await uow.BookingRepository.Update(entity);
		await uow.SaveChanges();

		if (wasCancelled)
			await publisher.Publish(new BookingCancelledEvent(entity.Id), cancellationToken);
	}
}
