using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Handlers.Booking.Delete;

public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand>
{
	private readonly IUnitOfWork uow;

	public DeleteBookingCommandHandler(IUnitOfWork uow)
	{
		this.uow = uow;
	}

	public async Task Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
	{
		var entity = await uow.BookingRepository.Get(request.Id);

		if (entity != null)
			await uow.BookingRepository.Delete(entity);
	}
}
