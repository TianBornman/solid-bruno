using Bruno.Domain.Exceptions;
using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Features.Booking.Commands.Delete;

public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand>
{
	private readonly IUnitOfWork uow;

	public DeleteBookingCommandHandler(IUnitOfWork uow)
	{
		this.uow = uow;
	}

	public async Task Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
	{
		var entity = await uow.BookingRepository.Get(request.Id) 
			?? throw new NotFoundException($"Booking not found."); ;

		entity.CanDelete();

		await uow.BookingRepository.Delete(entity);
	}
}
