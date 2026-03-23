using Bruno.Application.DTOs.Booking;
using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Handlers.Booking.List;

public class ListBookingCommandHandler : IRequestHandler<ListBookingCommand, List<GetBookingDto>>
{
	private readonly IUnitOfWork uow;

	public ListBookingCommandHandler(IUnitOfWork uow)
	{
		this.uow = uow;
	}

	public async Task<List<GetBookingDto>> Handle(ListBookingCommand request, CancellationToken cancellationToken)
	{
		var entities = await uow.BookingRepository.List(request.Skip, request.Take);

		var dtos = entities.Any() ? entities.Select(entity =>
									new GetBookingDto(entity.Id, entity.StartDate, entity.EndDate, entity.TotalPrice, entity.Status,
									entity.Vehicle.Id, entity.Customer.Id, entity.CreatedAt)).ToList() : [];

		return dtos;
	}
}
