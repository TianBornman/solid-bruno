using Bruno.Application.DTOs.Booking;
using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Handlers.Booking.Get;

public class GetBookingCommandHandler : IRequestHandler<GetBookingCommand, GetBookingDto?>
{
	private readonly IUnitOfWork uow;

	public GetBookingCommandHandler(IUnitOfWork uow)
	{
		this.uow = uow;
	}

	public async Task<GetBookingDto?> Handle(GetBookingCommand request, CancellationToken cancellationToken)
	{
		var entity = await uow.BookingRepository.Get(request.Id);

		if (entity == null)
			return null;

		var dto = new GetBookingDto(entity.Id, entity.StartDate, entity.EndDate, entity.TotalPrice, entity.Status,
					entity.Vehicle.Id, entity.Customer.Id, entity.CreatedAt);

		return dto;
	}
}
