using Bruno.Application.DTOs.Booking;
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
		var entities = await uow.BookingRepository.ListFiltered(request.Skip, request.Take, request.Status, request.Search);

		return entities.Select(GetBookingDto.FromEntity).ToList();
	}
}
