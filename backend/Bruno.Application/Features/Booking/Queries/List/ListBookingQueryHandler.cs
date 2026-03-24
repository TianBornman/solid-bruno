using Bruno.Application.DTOs.Booking;
using Bruno.Domain.Exceptions;
using Bruno.Domain.Repositories;
using MediatR;

namespace Bruno.Application.Features.Booking.Queries.List;

public class ListBookingQueryHandler : IRequestHandler<ListBookingQuery, List<GetBookingDto>>
{
	private readonly IUnitOfWork uow;

	public ListBookingQueryHandler(IUnitOfWork uow)
	{
		this.uow = uow;
	}

	public async Task<List<GetBookingDto>> Handle(ListBookingQuery request, CancellationToken cancellationToken)
	{
		var entities = await uow.BookingRepository.List(request.Skip, request.Take) 
			?? throw new NotFoundException("No bookings found.");

		var dtos = entities.Select(entity => new GetBookingDto(entity.Id, entity.DateRange.StartDate, entity.DateRange.EndDate, 
									entity.TotalPrice, entity.Status, entity.Vehicle.Id, entity.Customer.Id, entity.CreatedAt)).ToList();

		return dtos;
	}
}
